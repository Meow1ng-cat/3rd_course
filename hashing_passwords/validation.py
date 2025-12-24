from dataclasses import dataclass, field


ERR_LENGTH = "length"
ERR_LETTER = "requires_letter"
ERR_DIGIT = "requires_digit"
ERR_SPECIAL = "requires_special"


@dataclass
class PasswordValidationResult:
    is_valid: bool
    errors: list[str] = field(default_factory=list)
    warnings: list[str] = field(default_factory=list)

    def __bool__(self) -> bool:
        return self.is_valid


'''
Требуется проверить минимальную длину пароля (>= 12 символов) и
наличие в пароле хотя бы одной буквы, цифры и спецсимвола.
'''
def validate_password(password: str) -> PasswordValidationResult:
    errors = []
    
    # Проверка минимальной длины (>= 12 символов)
    if len(password) < 12:
        errors.append(ERR_LENGTH)
    
    # Проверка наличия хотя бы одной буквы
    has_letter = any(c.isalpha() for c in password)
    if not has_letter:
        errors.append(ERR_LETTER)
    
    # Проверка наличия хотя бы одной цифры
    has_digit = any(c.isdigit() for c in password)
    if not has_digit:
        errors.append(ERR_DIGIT)
    
    # Проверка наличия хотя бы одного спецсимвола
    # Спецсимвол - это любой символ, который не является буквой, цифрой или пробелом
    has_special = any(not c.isalnum() and not c.isspace() for c in password)
    if not has_special:
        errors.append(ERR_SPECIAL)
    
    is_valid = len(errors) == 0
    
    return PasswordValidationResult(is_valid=is_valid, errors=errors)
