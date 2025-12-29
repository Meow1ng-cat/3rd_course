def require_arg_types(*types_):
    def decorator(func):
        def wrapper(*args, **kwargs):
            if len(args) != len(types_):
                raise TypeError(f"Too many arguments! Given: {len(args)}, expected: {len(types_)}")
            for arg, i in zip(args, types_):
                if not isinstance(arg, i):
                    raise TypeError(f"{arg} doesn't match datatype {i}")
            return func(*args, **kwargs)
        return wrapper
    return decorator

@require_arg_types(int, str, float)
def pack(a, b, c):
    return a, b, c
try:
    print(pack(1, "x", 2.5))
except TypeError as e:
    print(e)
try:
    print(pack("1", "x", 2.5))
except TypeError as e:
    print(e)
try:
    print(pack(1, "x"))
except TypeError as e:
    print(e)