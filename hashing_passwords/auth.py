import hashlib
import time
from passlib.context import CryptContext
from validation import validate_password
from user import User, UserStorage

# Контекст для работы с bcrypt
bcrypt_context = CryptContext(schemes=["bcrypt"])


def register_user(storage: UserStorage, username: str, email: str, password: str) -> User:
    """
    Создает пользователя и сохраняет хэш пароля в виде bcrypt.
    """
    if User.exists(storage, username):
        raise ValueError("Пользователь с таким username уже существует")

    validation_result = validate_password(password)
    if not validation_result.is_valid:
        raise ValueError(f"Пароль не соответствует требованиям: {', '.join(validation_result.errors)}")

    # Используем bcrypt для хэширования пароля
    password_hash = bcrypt_context.hash(password)
    user = User(username=username, email=email, password_hash=password_hash, failed_attempts=0)
    user.save(storage)
    return user


def is_account_locked(storage: UserStorage, username: str) -> bool:
    """
    Проверяет, заблокирован ли аккаунт пользователя (после 5 неудачных попыток).
    """
    user = User.load(storage, username)
    if user is None:
        return False
    return user.failed_attempts >= 5


def verify_credentials(storage: UserStorage, username: str, password: str) -> bool:
    """
    Проверяет учетные данные пользователя.
    Возвращает True, если пользователь существует и пароль верный.
    Реализует ленивую миграцию MD5 -> bcrypt и блокировку после 5 неудачных попыток.
    """
    user = User.load(storage, username)
    if user is None:
        return False

    # Проверяем, не заблокирован ли аккаунт
    if is_account_locked(storage, username):
        return False

    # Проверяем, является ли хэш MD5 (32 символа hex)
    is_md5_hash = len(user.password_hash) == 32 and all(c in '0123456789abcdef' for c in user.password_hash.lower())
    
    if is_md5_hash:
        # Ленивая миграция: проверяем MD5 хэш
        md5_hex = hashlib.md5(password.encode("utf-8")).hexdigest()
        if user.password_hash == md5_hex:
            # Пароль верный - мигрируем на bcrypt
            user.password_hash = bcrypt_context.hash(password)
            user.failed_attempts = 0  # Сбрасываем счетчик при успешном входе
            user.save(storage)
            return True
        else:
            # Неверный пароль - увеличиваем счетчик неудачных попыток
            user.failed_attempts += 1
            user.save(storage)
            return False
    else:
        # Используем bcrypt для проверки
        if bcrypt_context.verify(password, user.password_hash):
            # Пароль верный - сбрасываем счетчик неудачных попыток
            user.failed_attempts = 0
            user.save(storage)
            return True
        else:
            # Неверный пароль - увеличиваем счетчик неудачных попыток
            user.failed_attempts += 1
            user.save(storage)
            return False
