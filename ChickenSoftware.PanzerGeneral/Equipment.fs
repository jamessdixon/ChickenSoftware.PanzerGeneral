
module ChickenSoftware.PanzerGeneral.Equipment

open Nation

type BaseEquipment =  {
    Id: int; Nation: Nation;
    IconId: int; 
    Description: string; Cost: int;
    YearAvailable:int; MonthAvailable: int;
    YearRetired: int; MonthRetired: int;
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
    Equipment: BaseEquipment; 
    EntrenchableEquipment: EntrenchableEquipment;
    MoveableEquipment: MoveableEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type TankEquipment = {
    Equipment: BaseEquipment; 
    MotorizedEquipment: MotorizedEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type ReconEquipment = {
    Equipment: BaseEquipment; 
    MotorizedEquipment: MotorizedEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type TankDestroyerEquipment = {
    Equipment: BaseEquipment; 
    MotorizedEquipment: MotorizedEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type AntiAirEquipment = {
    Equipment: BaseEquipment; 
    EntrenchableEquipment: EntrenchableEquipment;
    AirTargetCombatEquipment: AirTargetCombatEquipment
    }

type EmplacementEquipment = {
    Equipment: BaseEquipment; 
    LandTargetCombatEquipment: LandTargetCombatEquipment
    AirTargetCombatEquipment: AirTargetCombatEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }
    
type AntiTankEquipment = {
    Equipment: BaseEquipment; 
    EntrenchableEquipment: EntrenchableEquipment;
    MoveableEquipment: MoveableEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type ArtilleryEquipment = {
    Equipment: BaseEquipment
    MoveableEquipment: MoveableEquipment
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type SelfPropelledArtilleryEquipment = {
    ArtilleryEquipment: ArtilleryEquipment
    MotorizedEquipment: MotorizedEquipment
    }

type AirDefenseEquipment = {
    Equipment: BaseEquipment
    MoveableEquipment: MoveableEquipment
    AirTargetCombatEquipment: AirTargetCombatEquipment
    }

type TowedAirDefenseEquipment = {
    AirDefenseEquipment: AirDefenseEquipment
    EntrenchableEquipment: EntrenchableEquipment
    }

type SelfPropelledAirDefenseEquipment = {
    AirDefenseEquipment: AirDefenseEquipment
    MotorizedEquipment: MotorizedEquipment
    }
    
type AirFighterEquipment = {
    Equipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    LandTargetCombatEquipment: LandTargetCombatEquipment
    AirTargetCombatEquipment: AirTargetCombatEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }

type AirBomberEquipment = {
    Equipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    LandTargetCombatEquipment: LandTargetCombatEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }

type SubmarineEquipment = {
    Equipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }

type SurfaceShipEquipment = {
    Equipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    LandTargetCombatEquipment: LandTargetCombatEquipment
    AirTargetCombatEquipment: AirTargetCombatEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }

type AircraftCarrierEquipment = {
    Equipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    }

type TransportEquipment = {
    Equipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment;
    }