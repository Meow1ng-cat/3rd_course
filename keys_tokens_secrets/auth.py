from typing import Any, Tuple
import time

import crypto
import storage
import user

def record_token(payload: dict[str, Any]) -> None:
    db = storage.load_tokens()
    db["tokens"].append(payload)
    storage.save_tokens(db)

def revoke_by_jti(jti: str) -> None:
    db = storage.load_tokens()
    if "revoked" not in db:
        db["revoked"] = []
    if jti not in db["revoked"]:
        db["revoked"].append(jti)
        storage.save_tokens(db)

def is_revoked(jti: str) -> bool:
    db = storage.load_tokens()
    return jti in db.get("revoked", [])

def is_expired(exp: int) -> bool:
    return int(time.time()) >= exp

def login(username: str, password: str) -> Tuple[str, str]:
    u = user.get_user(username)
    if not u:
        raise ValueError("Invalid credentials")
    if not user.verify_password(u, password):
        raise ValueError("Invalid credentials")
    
    access_token, access_payload = crypto.issue_access(u.username)
    refresh_token, refresh_payload = crypto.issue_refresh(u.username)
    
    record_token(access_payload)
    record_token(refresh_payload)
    
    return access_token, refresh_token

def verify_access(access: str) -> dict[str, Any]:
    payload = crypto.decode(access)

    if payload.get("typ") != "access":
        raise ValueError("Wrong token type")

    if is_revoked(payload["jti"]):
        raise ValueError("Token revoked")

    if is_expired(payload["exp"]):
        raise ValueError("Token expired")
    
    return payload

def refresh_pair(refresh_token: str) -> Tuple[str, str]:
    """Обновляет пару токенов, отзывая старый refresh токен"""
    payload = crypto.decode(refresh_token)

    if payload.get("typ") != "refresh":
        raise ValueError("Wrong token type")

    if is_revoked(payload["jti"]):
        raise ValueError("Token revoked")

    if is_expired(payload["exp"]):
        raise ValueError("Token expired")

    revoke_by_jti(payload["jti"])

    sub = payload["sub"]
    access_token, access_payload = crypto.issue_access(sub)
    new_refresh_token, refresh_payload = crypto.issue_refresh(sub)
    
    record_token(access_payload)
    record_token(refresh_payload)
    
    return access_token, new_refresh_token

def revoke(token: str) -> None:
    """Отзывает токен (access или refresh)"""
    try:
        payload = crypto.decode(token)
        revoke_by_jti(payload["jti"])
    except Exception:
        pass

def introspect(token: str) -> dict[str, Any]:
    try:
        payload = crypto.decode(token)
        active = (not is_revoked(payload["jti"])) and (not is_expired(payload["exp"]))
        return {
            "active": active,
            "sub": payload.get("sub"),
            "typ": payload.get("typ"),
            "exp": payload.get("exp"),
            "jti": payload.get("jti"),
        }
    except Exception:
        return {"active": False, "error": "invalid_token"}
