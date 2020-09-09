class SectionBase(object):

    def __init__(self):
        self.area = 0.0
        self.unit_weight = 0.0
        self.moment_of_inertia_strong = 0.0
        self.moment_of_inertia_weak = 0.0

    def __str__(self):
        return "Area: %s\nMoment of Inertia around Strong Axis: %s\nMoment of Inertia around Weak Axis: %s" % (
        self.area, self.moment_of_inertia_strong, self.moment_of_inertia_weak)


class Rectangular(SectionBase):

    def __init__(self, width, height):
        super(Rectangular, self).__init__()
        self.width = width
        self.height = height
        self.area = width * height
        self.moment_of_inertia_strong = width * height ** 3.0 / 12.0
        self.moment_of_inertia_weak = height * width ** 3.0 / 12.0


class Square(Rectangular):

    def __init__(self, dimention):
        super(Square, self).__init__(dimention, dimention)


section = Rectangular(200, 500)
temp = section.__str__()
print(temp)
