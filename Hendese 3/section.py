import math
from enum import Enum


class SectionBase(object):

    def __init__(self):
        self.area = 0
        self.density = 0
        self.weight_per_meter = 0
        self.moment_of_inertia_strong = 0.0
        self.moment_of_inertia_weak = 0.0
        self.section_modulus_x = 0.0
        self.section_modulus_y = 0.0
        self.radius_of_gyration_x = 0.0
        self.radius_of_gyration_y = 0.0

    def __str__(self):
        return "Area: %s\nMoment of Inertia around Strong Axis: %s\nMoment of Inertia around Weak Axis: %s\nSection" \
               "Modulus - x: %s\nSection Modulus - y: %s\nRadius of Gyration - x: %s\nRadius of Gyration - y: %s\n" \
               "kg/m = %s\n" \
               % (self.area, self.moment_of_inertia_strong, self.moment_of_inertia_weak, self.section_modulus_x,
                  self.section_modulus_y, self.radius_of_gyration_x, self.radius_of_gyration_y, self.weight_per_meter)


class Rectangular(SectionBase): #RHS

    def __init__(self, height, width):
        super(Rectangular, self).__init__()
        self.width = width
        self.height = height
        self.area = width * height / 100
        self.moment_of_inertia_strong = width * height ** 3.0 / 12.0 / 10 ** 4
        self.moment_of_inertia_weak = height * width ** 3.0 / 12.0 / 10 ** 4
        self.section_modulus_x = self.moment_of_inertia_strong * 20 / height
        self.section_modulus_y = self.moment_of_inertia_weak * 20 / width
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = math.sqrt(self.moment_of_inertia_weak / self.area)
        self.weight_per_meter = self.density * self.area * 10


class Square(Rectangular): #SHS

    def __init__(self, dimension):
        super(Square, self).__init__(dimension, dimension)


class Pipe(SectionBase): # CHS

    def __init__(self, outer_diameter, thickness):
        super(Pipe, self).__init__()
        self.outer_diameter = outer_diameter
        self.thickness = thickness
        self.area = math.pi * (outer_diameter ** 2.0 - (outer_diameter - 2 * thickness) ** 2.0) / 400  # cm^2
        self.moment_of_inertia_strong = math.pi * (
                outer_diameter ** 4 - (outer_diameter - 2 * thickness) ** 4) / 64 * 1e-4
        self.moment_of_inertia_weak = self.moment_of_inertia_strong
        self.section_modulus_x = self.moment_of_inertia_strong * 20 / outer_diameter
        self.section_modulus_y = self.section_modulus_x
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = self.radius_of_gyration_x
        self.weight_per_meter = self.density * self.area * 10


class RoundBar(Pipe): #CSS

    def __init__(self, diameter):
        super(RoundBar, self).__init__(diameter, diameter / 2)


class IProfile(SectionBase): #ISection

    def __init__(self, height, width, tw, tf):
        super(IProfile, self).__init__()
        self.height = height
        self.width = width
        self.tw = tw
        self.tf = tf
        self.area = (width * tf * 2 + (height - 2 * tf) * tw) / 100
        self.moment_of_inertia_strong = (width * height ** 3 - (width - tw) * (height - 2 * tf) ** 3) / 120000
        self.moment_of_inertia_weak = (2 * width ** 3 * tf + (height - 2 * tf) * tw ** 3) / 120000
        self.section_modulus_x = 20 * self.moment_of_inertia_strong / height
        self.section_modulus_y = 20 * self.moment_of_inertia_weak / width
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = math.sqrt(self.moment_of_inertia_weak / self.area)
        self.weight_per_meter = self.density * self.area * 10


class TProfile(SectionBase): #TSection

    def __init__(self, height, width, tw, tf):
        super(TProfile, self).__init__()
        self.height = height
        self.width = width
        self.tw = tw
        self.tf = tf
        self.area = (width * tf + (height - tf) * tw) / 100
        self.eccentricity_from_bot = (tw * (height - tf) ** 2 / 2 + width * tf * (height - tf / 2)) / (
                1000 * self.area)
        self.eccentricity_from_top = height / 10 - self.eccentricity_from_bot
        self.moment_of_inertia_strong = (width * tf ** 3 / 12 + width * tf * (
                self.eccentricity_from_top - tf / 2) + tw
                                         * (height - tf) ** 3 / 12 + tw * (height - tf) * (
                                                 self.eccentricity_from_bot - (height - tf) / 2) ** 2) / 10000
        self.moment_of_inertia_weak = (tf * width ** 3 + (height - tf) * tw ** 3) / 120000
        self.section_modulus_x = self.section_modulus_x / self.eccentricity_from_bot
        self.section_modulus_y = 20 * self.moment_of_inertia_weak / width
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = math.sqrt(self.moment_of_inertia_weak / self.area)
        self.weight_per_meter = self.density * self.area * 10

    def __str__(self):
        return "Eccentricity from bot: %s\nEccentricity from top: %s" % (
            self.eccentricity_from_bot, self.eccentricity_from_top)


class BoxProfile(SectionBase): #RSS(Rectengular solid section)

    def __init__(self, height, width, tw, tf):
        super(BoxProfile, self).__init__()
        self.height = height
        self.width = width
        self.tw = tw
        self.tf = tf
        self.area = (height * width - (height - 2 * tf) * (width - 2 * tw)) / 100
        self.moment_of_inertia_strong = (width * height ** 3 - (width - 2 * tw) * (height - 2 * tf) ** 3) / 120000
        self.moment_of_inertia_weak = (height * width ** 3 - (height - 2 * tf) * (width - 2 * tw) ** 3) / 120000
        self.section_modulus_x = 20 * self.moment_of_inertia_strong / height
        self.section_modulus_y = 20 * self.moment_of_inertia_weak / width
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = math.sqrt(self.moment_of_inertia_weak / self.area)
        self.weight_per_meter = self.density * self.area * 10


class UProfile(SectionBase): # U section

    def __init__(self, height, width, thickness):
        super(UProfile, self).__init__()
        self.height = height
        self.width = width
        self.thickness = thickness
        self.ridge = self.thickness * 1.5
        self.area = (2 * (width - thickness - self.ridge) * thickness + (height - 2 * (self.ridge + thickness)) *
                     thickness + math.pi * ((self.ridge + thickness) ** 2 - self.ridge ** 2) / 2) / 100
        self.eccentricity_from_body = ((2 * (width - thickness) * thickness * ((width - thickness) / 2 + thickness)) +
                                       ((height - 2 * thickness) * thickness ** 2 / 2)) / (
                                              (2 * (width - thickness) * thickness) + (
                                              (height - 2 * thickness) * thickness)) / 10
        self.moment_of_inertia_strong = (width * height ** 3 - (width - thickness) * (
                height - 2 * thickness) ** 3) / 120000
        self.moment_of_inertia_weak = ((height - 2 * thickness) * thickness ** 3 / 12 + (height - 2 * thickness) *
                                       thickness * (self.eccentricity_from_body * 10 - thickness / 2) ** 2 + 2 *
                                       thickness * (width - thickness) ** 3 / 12 + 2 * thickness * (width - thickness) *
                                       ((
                                                width - thickness) / 2 + thickness - self.eccentricity_from_body * 10) ** 2) / 10000
        self.section_modulus_x = 20 * self.moment_of_inertia_strong / height
        self.section_modulus_y = self.moment_of_inertia_weak / (width / 10 - self.eccentricity_from_body)
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = math.sqrt(self.moment_of_inertia_weak / self.area)
        self.weight_per_meter = self.density * self.area * 10


class CProfile(SectionBase): # CSection

    def __init__(self, height, width, thickness, c):
        super(CProfile, self).__init__()
        self.height = height
        self.width = width
        self.thickness = thickness
        self.c = c
        self.s = height - 2 * c
        self.ridge = thickness
        self.area = (2 * (width - 2 * (self.ridge + thickness)) * thickness + 2 * (
                height - 2 * (self.ridge + thickness))
                     * thickness + math.pi * (
                             (self.ridge + thickness) ** 2 - self.ridge ** 2) - self.s * thickness) / 100
        self.length = self.area / thickness * 100
        self.eccentricity_from_body = ((thickness * (self.s + self.length) * width / 2 - self.s * thickness * (width -
                                                                                                               thickness / 2)) / (
                                               self.area * 100)) / 10
        self.moment_of_inertia_strong = (width * height ** 3 - (width - 2 * thickness) * (height - 2 * thickness) ** 3
                                         - thickness * self.s ** 3) / 120000
        self.moment_of_inertia_weak = ((height - 2 * thickness) * (thickness ** 3) / 12 + (height - 2 * thickness) *
                                       thickness * (self.eccentricity_from_body * 10 - thickness / 2) ** 2 + 2 *
                                       thickness * (width - 2 * thickness) ** 3 / 12 + 2 * thickness * (width - 2 *
                                                                                                        thickness) * (
                                               width / 2 - self.eccentricity_from_body * 10) ** 2 + 2 *
                                       thickness * (c - thickness - self.ridge) ** 3 + 2 * thickness * (c - thickness -
                                                                                                        self.ridge) * (
                                               width - thickness / 2 - self.eccentricity_from_body) ** 2) / 10000
        self.section_modulus_x = 20 * self.moment_of_inertia_strong / height
        self.section_modulus_y = self.moment_of_inertia_weak / (width / 10 - self.eccentricity_from_body)
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = math.sqrt(self.moment_of_inertia_weak / self.area)
        self.weight_per_meter = self.density * self.area * 10


class LProfile(SectionBase): #AngularSection

    def __init__(self, height, width, thickness):
        super(LProfile, self).__init__()
        self.height = height
        self.width = width
        self.thickness = thickness
        self.ridge = thickness
        self.area = thickness * (height + width - 4 * thickness + math.pi * (self.ridge + thickness + 2) / 2) / 100
        self.length = self.area / thickness * 100
        self.eccentricity_x = (thickness ** 2 * width + height * thickness) / (self.area * 2000)
        self.eccentricity_y = (height * thickness * thickness / 2 + thickness * width * width / 2) / (
                self.length * thickness * 10)
        self.moment_of_inertia_strong = (thickness * height ** 3 + width * thickness ** 3) / 120000 + height * thickness \
                                        * (
                                                height / 20 - self.eccentricity_x) ** 2 / 100 + width * thickness ** 3 / 40000
        self.moment_of_inertia_weak = (thickness * width ** 3 + height * thickness ** 3) / 120000 + width * thickness * \
                                      (width / 2 - self.eccentricity_y * 10) ** 2 / 10000 + height * thickness * (
                                              self.eccentricity_y * 10 -
                                              thickness / 2) ** 2 / 10000
        self.section_modulus_x = self.moment_of_inertia_strong / (height / 10 - self.eccentricity_x)
        self.section_modulus_y = self.moment_of_inertia_weak / (width / 10 - self.eccentricity_y)
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = math.sqrt(self.moment_of_inertia_weak / self.area)
        self.weight_per_meter = self.density * self.area * 10


class IsoscelesTriangle(SectionBase):

    def __init__(self, height, width, t1, t2):
        super(IsoscelesTriangle, self).__init__()
        self.height = height
        self.width = width
        self.t1 = t1
        self.t2 = t2
        self.edge = math.sqrt(width ** 2 / 4 + height ** 2)
        self.length = 2 * self.edge + width
        self.area = (2 * self.edge * t2 + width * t1) / 100
        self.eccentricity_from_bot = (width * t1 * t1 / 2 + 2 * self.edge * t2 * height / 2) / (self.area * 100)
        self.eccentricity_from_top = height - self.eccentricity_from_bot
        self.eccentricity_max = max(self.eccentricity_from_top, self.eccentricity_from_bot)
        self.moment_of_inertia_strong = (width * t1 * t1 * t1 / 12 + width * t1 * (
                self.eccentricity_from_bot - t1 / 2) * (self.eccentricity_from_bot - t1 / 2)) / \
                                        10000 + 2 * ((
                                                             t2 * self.edge / height) * height * height * height / 12 + t2 * self.edge * (
                                                             height / 2 - self.eccentricity_from_bot) * (
                                                             height / 2 - self.eccentricity_from_bot)) / 1000
        self.moment_of_inertia_weak = t1 * height * height * height / (12 * 1000) + 2 * (
                t1 * self.edge / (height / 2) * (height / 2) *
                (height / 2) * (height / 2) / (12 * 1000) + self.edge * t2 * (width / 4) * (width / 4) / 1000)
        self.section_modulus_x = self.moment_of_inertia_strong / (self.eccentricity_max / 10)
        self.section_modulus_y = self.moment_of_inertia_weak / ((width / 10) / 2)
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = math.sqrt(self.moment_of_inertia_weak / self.eccentricity_from_top)
        self.weight_per_meter = self.density * self.area * 10


class TwoStackedBoxes(SectionBase):  # sonlarda bazı sorunlar var

    def __init__(self, height1, width1, tw1, tf1, height2, width2, tw2, tf2):
        super(TwoStackedBoxes, self).__init__()
        self.height1 = height1
        self.height2 = height2
        self.width1 = width1
        self.width2 = width2
        self.tw1 = tw1
        self.tw2 = tw2
        self.tf1 = tf1
        self.tf2 = tf2
        self.height_comp = height1 + height2
        self.width_comp = max(width1, width2)
        self.area1 = (width1 * height1 - (width1 - 2 * tw1) * (height1 - 2 * tf1)) / 100
        self.area2 = (width2 * height2 - (width2 - 2 * tw2) * (height2 - 2 * tf2)) / 100
        self.area = self.area1 + self.area2
        self.moment_of_inertia_strong1 = (width1 * height1 ** 3 - (width1 - 2 * tw1) * (
                height1 - 2 * tf1) ** 3) / 120000
        self.moment_of_inertia_weak1 = (height1 * width1 ** 3 - (height1 - 2 * tf1) * (width1 - 2 * tw1) ** 3) / 120000
        self.moment_of_inertia_strong2 = (width2 * height2 ** 3 - (width2 - 2 * tw2) * (
                height2 - 2 * tf2) ** 3) / 120000
        self.moment_of_inertia_weak2 = (height2 * width2 ** 3 - (height2 - 2 * tf2) * (width2 - 2 * tw2) ** 3) / 120000
        self.moment_of_inertia_weak = self.moment_of_inertia_weak1 + self.moment_of_inertia_weak2
        self.eccentricity1 = (self.area2 * height2 / 2 + self.area1 * (height2 + height1 / 2)) / (self.area * 10)
        self.eccentricity2 = self.height_comp / 10 - self.eccentricity1
        self.r1 = (height2 + height1 / 2 - self.eccentricity1 * 10) / 10
        self.r2 = self.eccentricity1 - height2 / 20
        self.moment_of_inertia_xe1 = self.moment_of_inertia_strong1 + self.area1 * self.r1 ** 2
        self.moment_of_inertia_xe2 = self.moment_of_inertia_strong2 + self.area2 * self.r2 ** 2
        self.moment_of_inertia_strong = self.moment_of_inertia_xe1 + self.moment_of_inertia_xe2
        self.section_modulus_x = 20 * self.moment_of_inertia_strong / self.height_comp
        self.section_modulus_y = 20 * self.moment_of_inertia_weak / self.width_comp
        self.radius_of_gyration_x = math.sqrt(self.moment_of_inertia_strong / self.area)
        self.radius_of_gyration_y = math.sqrt(self.moment_of_inertia_weak / self.area)
        self.weight_per_meter = self.density * self.area * 10


class BoltResistance(object):

    def __init__(self):
        self.tensile = 0
        self.shear = 0

    def __str__(self):
        return "Tensile Strength (kg): %s\n Shear Strength (kg): %s" % (self.tensile, self.shear)


class Trial(BoltResistance):

    def __init__(self, grade, stressarea, nominaldiameter):
        super(Trial, self).__init__()
        self.grade = grade
        self.StressArea = stressarea
        self.NominalDiameter = nominaldiameter
        self.tensile_resistance_factor = 0.9
        self.shear_resistance_factor = 0.5
        if self.grade == 4.6:
            self.UTS = 400 * 100 / 9.81
            self.shear_resistance_factor = 0.6
        elif self.grade == 4.8:
            self.UTS = 400 * 100 / 9.81
        elif self.grade == 5.6:
            self.UTS = 500 * 100 / 9.81
            self.shear_resistance_factor = 0.6
        elif self.grade == 5.8:
            self.UTS = 500 * 100 / 9.81
        elif self.grade == 6.8:
            self.UTS = 600 * 100 / 9.81
        elif self.grade == 8.8:
            self.UTS = 800 * 100 / 9.81
            self.shear_resistance_factor = 0.6
        elif self.grade == 10.9:
            self.UTS = 1000 * 100 / 9.81
        else:
            self.UTS = False
        self.YS = self.UTS * (grade - math.floor(grade))
        self.tensile = self.UTS * self.tensile_resistance_factor * stressarea / 100 / 1.25
        self.shear = self.UTS * self.shear_resistance_factor * stressarea / 100 / 1.25


class PlaneSectionForce(object):

    def __init__(self):
        self.V2 = 0.0
        self.M3 = 0.0


class SectionForces(PlaneSectionForce):

    def __init__(self):
        super(SectionForces, self).__init__()
        self.N = 0.0
        self.V2 = 0.0
        self.T = 0.0
        self.M2 = 0.0





class Material(object):

    def __init__(self, rho, modulus_of_elasticity, nu):
        self.rho = rho  # unit weight
        self.modulus_of_elasticity = modulus_of_elasticity
        self.nu = nu #poisson ratio


class SteelMaterial(Material):

    def __init__(self, yield_stress, tensile_stress):
        super(SteelMaterial, self).__init__(78.5, 210, 0.3) # kN/m^3 , GPa
        self.f_y = yield_stress
        self.f_u = tensile_stress

SteelMaterial.S235 = SteelMaterial(235, 310)
SteelMaterial.S275 = SteelMaterial(275, 460)
SteelMaterial.S355 = SteelMaterial(355, 510)


class SimpleSupportedBeamTypes(Enum):
    UNIFORM_DISTRIBUTED_LOADED = 1
    MID_POINT_LOADED = 2


class BeamBase(object):

    def __init__(self, material, section):
        self.support_reactions = []
        self.beam_internal_forces = dict()
        self.length = 0.0
        self.material = material
        self.section = section

    def get_maximum_moment(self):
        raise NotImplemented("this method should be implemented")

    def get_maximum_shear(self):
        raise NotImplemented("this method should be implemented")

    def get_maximum_deflection(self):
        raise NotImplemented("this method should be implemented")


class SimpleSupportedBeam(BeamBase):

    def __init__(self, beam_type, length, load_value, material, section):  # length in mm, load in kN & m
        super(SimpleSupportedBeam, self).__init__(material, section)
        self.beam_type = beam_type
        self.length = length
        self.load = load_value

    def get_maximum_moment(self):
        if self.beam_type == SimpleSupportedBeamTypes.UNIFORM_DISTRIBUTED_LOADED:
            ret = PlaneSectionForce()
            ret.M3 = self.load * self.length ** 2.0 / 8.0
            ret.V2 = 0
            return ret
        elif self.beam_type == SimpleSupportedBeamTypes.MID_POINT_LOADED:
            pass
        else:
            raise NotImplemented("The given beam type(%) is not implemented") % self.beam_type

    def get_maximum_shear(self):
        if self.beam_type == SimpleSupportedBeamTypes.UNIFORM_DISTRIBUTED_LOADED:
            ret = PlaneSectionForce()
            ret.M3 = 0
            ret.V2 = self.load * self.length / 2.0
            return ret
        elif self.beam_type == SimpleSupportedBeamTypes.MID_POINT_LOADED:
            pass
        else:
            raise NotImplemented("The given beam type(%) is not implemented") % self.beam_type

    def get_maximum_deflection(self):
        if self.beam_type == SimpleSupportedBeamTypes.UNIFORM_DISTRIBUTED_LOADED:
            return 5/384 * self.load * self.length ** 4.0 / (self.material.modulus_of_elasticity * self.section.moment_of_inertia_strong)
        elif self.beam_type == SimpleSupportedBeamTypes.MID_POINT_LOADED:
            pass
        else:
            raise NotImplemented("The given beam type(%) is not implemented") % self.beam_type


def TestSimpleSupported():
    material = SteelMaterial.S235
    section = Rectangular(300, 500)
    beam = SimpleSupportedBeam(SimpleSupportedBeamTypes.UNIFORM_DISTRIBUTED_LOADED, 3000, 10, material, section )
    print("maximum moment :", beam.get_maximum_moment().M3)
    print("maximum shear :", beam.get_maximum_shear().V2)
    print("maximum deflection :", beam.get_maximum_deflection())


TestSimpleSupported()

# section = Rectangular(40, 14)
# print(section.__str__())

# section1 = Pipe(21.3, 3.2)
# print(section1.__str__())

# section2 = RoundBar(20)
# print(section2.__str__())

# section3 = IProfile(500, 250, 10, 12)
# print(section3.__str__())

# section4 = BoxProfile(450, 350, 6, 6)
# print(section4.__str__())

# section5 = UProfile(300, 150, 30)
# print(section5.__str__())

# section6 = CProfile(250, 75, 2, 15)
# print(section6.__str__())

# section7 = LProfile()
# print(section7.__str__())

# section8 = IsoscelesTriangle(800, 750, 10, 10)
# print(section8.__str__())

# section9 = TwoStackedBoxes(100, 100, 4, 4, 80, 80, 3, 3)
# print(section9.__str__())

# section10 = Trial(5.6, 58, 4)
# print(section10.__str__())
