class Athlete:
    def __init__(self, personal_best):
        self.personal_best = personal_best

class Sprinter(Athlete):
    def __init__(self, personal_best, sprint_distance):
        super().__init__(personal_best)
        self.sprint_distance = sprint_distance

    def improve_performance(self):
        if self.sprint_distance > self.personal_best:
            previous_best = self.personal_best
            self.personal_best = self.sprint_distance
            return f"New personal best: {self.personal_best}! Previous best: {previous_best}"
        else:
            return "Personal best not broken."

class Jumper(Athlete):
    def __init__(self, personal_best, jump_category):
        super().__init__(personal_best)
        self.jump_category = jump_category

    def improve_performance(self):
        if self.jump_category > self.personal_best:
            previous_best = self.personal_best
            self.personal_best = self.jump_category
            return f"New personal best: {self.personal_best}! Previous best: {previous_best}"
        else:
            return "Personal best not broken."

if __name__ == "__main__":
    athlete = Athlete(100)
    jumper = Jumper(4, 5)
    sprinter = Sprinter(100, 250)
    print(jumper.improve_performance())
    print(sprinter.improve_performance())
    jumper.jump_category = 3
    sprinter.sprint_distance = 100
    print(jumper.improve_performance())
    print(sprinter.improve_performance())

