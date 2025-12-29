class University:
    def __init__(self, name, address, budget, faculties):
        self.name = name
        self.address = address
        self.budget = budget
        self.faculties = faculties

    def __str__(self):
        return f"Название: {self.name} \nАдрес: {self.address} \nБюджет: {self.budget} \nФакультеты: {self.faculties}"

class Faculty:
    def __init__(self, name, specialty, professors, students):
        self.name = name
        self.specialty = specialty
        self.professors = professors
        self.students = students

    def __str__(self):
        return f"Название: {self.name} \nСпециализация {self.specialty} \nПрофессоры: {self.professors} \nСтуденты: {self.students}"

class Student:
    def __init__(self, name, group, gpa):
        self.name = name
        self.group = group
        self.gpa = gpa

    def __str__(self):
        return f"Имя: {self.name} \nГруппа {self.group} \nУспеваемость: {self.gpa}"

if __name__ == "__main__":
    mpsu = University("МПГУ", "пр. Вернадского, 88, Moscow, 119571", 45000000, [])
    iie = Faculty("ИМО", ["Лингвистика", "Журналистика", "IT", "Педагогическое образование"], 34, 997)
    student1 = Student("Тимур", "334", 4.0)
    mpsu.faculties.append(iie.name)
    print(mpsu.__str__() + "\n")
    print(iie.__str__() + "\n")
    print(student1.__str__())
