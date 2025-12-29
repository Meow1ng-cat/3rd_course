def run_once(func):
    result = None
    has_run = False

    def wrapper(*args, **kwargs):
        nonlocal has_run, result
        if not has_run:
            result = func(*args, **kwargs)
            has_run = True
        return result

    return wrapper

calls = 0

@run_once
def init():
    global calls
    calls += 1
    print("running...")
    return "ready"

print(init())   # ready
print(init())   # ready (результат из памяти, без "running...")
print(calls)    # 1