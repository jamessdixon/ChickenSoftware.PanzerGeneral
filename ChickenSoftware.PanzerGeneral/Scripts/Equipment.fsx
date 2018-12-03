
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Scripts/Nation.fsx"

open Nation

type Equipment =  {
    Id: int; 
    Nation: Nation; 
    Description: string; 
    Cost: int;
    YearAvailable:int; 
    MonthAvailable: int;
    YearRetired: int; 
    MonthRetired: int;
    MaximumSpottingRange: int;
    GroundDefensePoints: int; 
    AirDefensePoints: int
    NavalDefensePoints: int
    }
    
type EntrenchableEquipment = {
    EntrenchmentRate: int
    }

type MoveableEquipment = {
    MaximumMovementPoints: int;
    }

type MotorizedEquipment = {
    MoveableEquipment: MoveableEquipment
    MaximumFuel: int}

type CombatEquipment = {
    MaximumAmmo: int;
    Initative: int;
    }

type LandTargetCombatEquipment = {
    CombatEquipment: CombatEquipment;
    HardAttackPoints: int;
    SoftAttackPoints: int;
    }

type AirTargetCombatEquipment = {
    CombatEquipment: CombatEquipment;
    AirAttackPoints: int;
    }

type NavalTargetCombatEquipment = {
    CombatEquipment: CombatEquipment;
    NavalAttackPoints: int
    }

type InfantryEquipment = {
    Equipment: Equipment; 
    EntrenchableEquipment: EntrenchableEquipment;
    MoveableEquipment: MoveableEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type TankEquipment = {
    Equipment: Equipment; 
    MotorizedEquipment: MotorizedEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type ReconEquipment = {
    Equipment: Equipment; 
    MotorizedEquipment: MotorizedEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type AntiArmorEquipment = {
    Equipment: Equipment; 
    MotorizedEquipment: MotorizedEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type AntiAirEquipment = {
    Equipment: Equipment; 
    EntrenchableEquipment: EntrenchableEquipment;
    AirTargetCombatEquipment: AirTargetCombatEquipment
    }

type EmplacementEquipment = {
    Equipment: Equipment; 
    LandTargetCombatEquipment: LandTargetCombatEquipment
    AirTargetCombatEquipment: AirTargetCombatEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }
    
type Infantry =
| Basic of InfantryEquipment
| Engineer of InfantryEquipment
| Airborne of InfantryEquipment
| Bridging of InfantryEquipment

type Tank = | Tank of TankEquipment
type Recon = | Recon of ReconEquipment
type AntiArmor = | AntiArmor of AntiArmorEquipment
type AntiAir = | AntiAir of AntiAirEquipment
type Emplacement = Emplacement of EmplacementEquipment

type Caliber =
| Light
| Heavy

type Propulsion =
| Towed
| SelfPropelled

type AntiTankEquipment = {
    Equipment: Equipment; 
    LandTargetCombatEquipment: LandTargetCombatEquipment;
    Caliber: Caliber;
    Propulsion: Propulsion
    }

type AntiTank = | AntiTank of AntiTankEquipment

type ArtilleryEquipment = {
    Equipment: Equipment; 
    LandTargetCombatEquipment: LandTargetCombatEquipment;
    Caliber: Caliber;
    Propulsion: Propulsion
    }

type Artillery = | Artillery of ArtilleryEquipment

type AirDefenseEquipmnet = {
    Equipment: Equipment; 
    AirTargetCombatEquipment: AirTargetCombatEquipment
    Propulsion: Propulsion
    }

type AirDefense = | AirDefense of AirDefenseEquipmnet

type LandCombat =
| Infantry of Infantry
| Tank of Tank
| Recon of Recon
| AntiTank of AntiTank
| Artillery of Artillery
| AntiAir of AntiAir
| AirDefense of AirDefense
| Emplacement of Emplacement

type Fighter = 
| Prop
| Jet

type Bomber =
| Strategic 
| Tactical 

type AirCombat =
| Fighter of Fighter
| Bomber of Bomber
| Transport

type NavalCombat =
| Submarine
| Destroyer
| CapitalShip
| AircraftCarrier
| Transport

type Combat =
| Land of LandCombat
| Air of AirCombat
| Naval of NavalCombat

type Transport =
| Land
| Air
| Naval

type EquipmentType =
| Combat of Combat
| Transport of Transport

//UnitType: UnitType
//TargetType: TargetType
            
//CanBridgeRivers
//CanParadrop
//CanHaveAirTransport
//CanHaveOrganicTransport
//CanHaveSeaTransport
//IgnoresEntrenchment
//JetInd

//MovementTypeId
//MaxFuel

//NavalAttack
//HardAttack
//SoftAttack
//MaxAmmo
//Initative
//AirAttack

//Id
//Description
//Movement
//LastYear
//Year
//Month
//NationId
//Range
//Spotting
//Cost
//Icon
//StackedIcon
//TargetTypeId
//CloseDefense
//GroundDefense
//AirDefense

//type SoftTarget =
//| Infantry of Infantry
//| AntiAir of AntiAir
//| Emplacement of Emplacement
//| AntiTank of AntiTank
//| Artillery of Artillery
//| AirDefense of AirDefense

//type HardTarget =
//| Tank of Tank
//| Recon of Recon
//| AntiArmor of AntiArmor

//type TargetType =
//| Soft of SoftTarget
//| Hard of HardTarget