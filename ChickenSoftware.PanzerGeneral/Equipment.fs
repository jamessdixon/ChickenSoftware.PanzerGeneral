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

type FullTrackEquipment = | FullTrackEquipment of MotorizedEquipment
type HalfTrackEquipment = | HalfTrackEquipment of MotorizedEquipment
type WheeledEquipment = | WheeledEquipment of MotorizedEquipment

type TrackedEquipment =
| FullTrack of FullTrackEquipment
| HalfTrack of HalfTrackEquipment

type LandMotorizedEquipment =
| Tracked of TrackedEquipment
| Wheeled of WheeledEquipment

type SeaMoveableEquipment = {MotorizedEquipment: MotorizedEquipment}
type AirMoveableEquipment = {MoveableEquipment: MoveableEquipment}
type CombatEquipment = {MaximumAmmo: int;Initative: int;}

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
    BaseEquipment: BaseEquipment; 
    EntrenchableEquipment: EntrenchableEquipment;
    MoveableEquipment: MoveableEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type TankEquipment = {
    BaseEquipment: BaseEquipment; 
    FullTrackedEquipment: FullTrackEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type ReconEquipment = {
    BaseEquipment: BaseEquipment; 
    LandMotorizedEquipment: LandMotorizedEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }
    
type AntiAirEquipment = {
    BaseEquipment: BaseEquipment; 
    TrackedEquipment: TrackedEquipment;
    AirTargetCombatEquipment: AirTargetCombatEquipment
    }

type EmplacementEquipment = {
    BaseEquipment: BaseEquipment; 
    LandTargetCombatEquipment: LandTargetCombatEquipment
    AirTargetCombatEquipment: AirTargetCombatEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }
    
type AntiTankEquipment = {
    BaseEquipment: BaseEquipment; 
    EntrenchableEquipment: EntrenchableEquipment;
    MoveableEquipment: MoveableEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type TankDestroyerEquipment = {
    BaseEquipment: BaseEquipment; 
    FullTrackedEquipment: FullTrackEquipment;
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type ArtilleryEquipment = {
    BaseEquipment: BaseEquipment
    MoveableEquipment: MoveableEquipment
    LandTargetCombatEquipment: LandTargetCombatEquipment
    }

type SelfPropelledArtilleryEquipment = {
    ArtilleryEquipment: ArtilleryEquipment
    TrackedEquipment: TrackedEquipment
    }

type AirDefenseEquipment = {
    BaseEquipment: BaseEquipment
    MoveableEquipment: MoveableEquipment
    AirTargetCombatEquipment: AirTargetCombatEquipment
    }

type TowedAirDefenseEquipment = {
    AirDefenseEquipment: AirDefenseEquipment
    EntrenchableEquipment: EntrenchableEquipment
    }

type SelfPropelledAirDefenseEquipment = {
    AirDefenseEquipment: AirDefenseEquipment
    TrackedEquipment: TrackedEquipment
    }
    
type AirFighterEquipment = {
    BaseEquipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    LandTargetCombatEquipment: LandTargetCombatEquipment
    AirTargetCombatEquipment: AirTargetCombatEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }

type AirBomberEquipment = {
    BaseEquipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    LandTargetCombatEquipment: LandTargetCombatEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }

type SubmarineEquipment = {
    BaseEquipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }

type SurfaceShipEquipment = {
    BaseEquipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    LandTargetCombatEquipment: LandTargetCombatEquipment
    AirTargetCombatEquipment: AirTargetCombatEquipment
    NavalTargetCombatEquipment: NavalTargetCombatEquipment
    }

type AircraftCarrierEquipment = {
    BaseEquipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment
    }

type LandTransportEquipment = {
    BaseEquipment: BaseEquipment
    TrackedEquipment: TrackedEquipment
    }

type AirTransportEquipment = {
    BaseEquipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment;
    }

type SeaTransportEquipment = {
    BaseEquipment: BaseEquipment
    MotorizedEquipment: MotorizedEquipment;
    }