
module ChickenSoftware.PanzerGeneral.Unit

open Equipment

type UnitStats = {
    Id: int; 
    Name: string; 
    Strength: int;
    }
    
type ReinforcementType =
| Core
| Auxiliary

type CombatStats = {
    Ammo: int;
    Experience:int;
    ReinforcementType:ReinforcementType    }

type MotorizedMovementStats = {
    Fuel: int;}

type InfantryUnit = {UnitStats: UnitStats; CombatStats: CombatStats; Equipment: InfantryEquipment; 
    CanBridge: bool; CanParaDrop: bool}
type TankUnit = {UnitStats: UnitStats; CombatStats: CombatStats; MotorizedMovementStats:MotorizedMovementStats; 
    Equipment: TankEquipment}
type ReconUnit = {UnitStats: UnitStats; CombatStats: CombatStats; MotorizedMovementStats:MotorizedMovementStats; 
    Equipment: ReconEquipment}
type TankDestroyerUnit = {UnitStats: UnitStats; CombatStats: CombatStats; MotorizedMovementStats:MotorizedMovementStats; 
    Equipment: TankDestroyerEquipment}
type AntiAirUnit = {UnitStats: UnitStats; CombatStats: CombatStats; MotorizedMovementStats:MotorizedMovementStats;
    Equipment: AntiAirEquipment}
type EmplacementUnit = {UnitStats: UnitStats; CombatStats: CombatStats; Equipment: EmplacementEquipment}
type TowedAirDefenseUnit = {UnitStats: UnitStats; CombatStats: CombatStats; Equipment: TowedAirDefenseEquipment}
type SelfPropelledAirDefenseUnit = {UnitStats: UnitStats; CombatStats: CombatStats; 
    MotorizedMovementStats:MotorizedMovementStats; Equipment: SelfPropelledAirDefenseEquipment}
type TowedArtilleryUnit = {UnitStats: UnitStats; CombatStats: CombatStats; Equipment: ArtilleryEquipment}
type SelfPropelledArtilleryUnit = {UnitStats: UnitStats; CombatStats: CombatStats; 
    MotorizedMovementStats:MotorizedMovementStats; Equipment: SelfPropelledArtilleryEquipment}
type AntiTankUnit = {UnitStats: UnitStats; CombatStats: CombatStats; Equipment: AntiTankEquipment}

type Infantry =
| Basic of InfantryUnit
| HeavyWeapon of InfantryUnit
| Engineer of InfantryUnit
| Airborne of InfantryUnit
| Ranger of InfantryUnit
| Bridging of InfantryUnit

type AirDefense = 
| Towed of TowedAirDefenseUnit
| SelfPropelled of SelfPropelledAirDefenseUnit

type TowedArtillery =
| Light of TowedArtilleryUnit
| Heavy of TowedArtilleryUnit

type Artillery = 
| Towed of TowedArtillery
| SelfPropelled of SelfPropelledArtilleryUnit

type AntiTank = 
| Light of AntiTankUnit
| Heavy of AntiTankUnit

type Emplacement =
| Strongpoint of EmplacementUnit
| Fort of EmplacementUnit

type LandCombat =
| Infantry of Infantry
| Tank of TankUnit
| Recon of ReconUnit
| TankDestroyer of TankDestroyerUnit
| AntiAir of AntiAirUnit
| Emplacement of Emplacement
| AirDefense of AirDefense
| AntiTank of AntiTank
| Artillery of Artillery

type AirFighterUnit = {UnitStats: UnitStats; CombatStats: CombatStats; MotorizedMovementStats:MotorizedMovementStats; Equipment: AirFighterEquipment;}
type AirBomberUnit = {UnitStats: UnitStats; CombatStats: CombatStats; MotorizedMovementStats:MotorizedMovementStats; Equipment: AirBomberEquipment;}

type Fighter = 
| Prop of AirFighterUnit
| Jet of AirFighterUnit

type Bomber =
| Strategic of AirBomberUnit
| Tactical of AirBomberUnit

type AirCombat =
| Fighter of Fighter
| Bomber of Bomber

type SubmarineUnit = {UnitStats: UnitStats; CombatStats: CombatStats; MotorizedMovementStats:MotorizedMovementStats; Equipment: SubmarineEquipment;}
type SurfaceShipEquipmentUnit = {UnitStats: UnitStats; CombatStats: CombatStats; MotorizedMovementStats:MotorizedMovementStats; Equipment: SurfaceShipEquipment;}

type NavalCombat =
| Submarine of SubmarineUnit
| Destroyer of SurfaceShipEquipmentUnit
| CapitalShip of SurfaceShipEquipmentUnit

type LandTransportPayload =
| Infanty of Infantry
| AntiTank of AntiTank
| Artillery of TowedArtillery
| AirDefense of TowedAirDefenseUnit

type AirTransportPayload = 
| Airborne of InfantryUnit
| Ranger of InfantryUnit

type NavalTransportPayload =
| Infantry of Infantry
| Tank of TankUnit
| TankDestroyer of TankDestroyerUnit
| Recon of ReconUnit
| AntiTank of AntiTank
| Artillery of Artillery
| AntiAir of AntiAirUnit
| AirDefense of AirDefense

type AircraftCarrierPayload =
| PropFighter of AirFighterUnit
| TacticalBomber of AirBomberUnit

type Combat =
| Land of LandCombat
| Air of AirCombat
| Naval of NavalCombat

type LandTransportUnit = {UnitStats: UnitStats; MotorizedMovementStats:MotorizedMovementStats;Equipment: LandTransportEquipment; Payload: LandTransportPayload option}
type AirTransportUnit = {UnitStats: UnitStats; MotorizedMovementStats:MotorizedMovementStats;Equipment: AirTransportEquipment; Payload: AirTransportPayload option}
type SeaTransportUnit = {UnitStats: UnitStats; MotorizedMovementStats:MotorizedMovementStats;Equipment: SeaTransportEquipment}
type LandingCraftUnit = {TransportUnit: SeaTransportUnit; Payload: NavalTransportPayload option}
type AircraftCarrierUnit = {TranportUnit: SeaTransportUnit; Payload: AircraftCarrierPayload list option}

type NavalTransport =
| LandingCraft of LandingCraftUnit
| AircraftCarrier of AircraftCarrierUnit

type Transport =
| Land of LandTransportUnit
| Air of AirTransportUnit
| Naval of NavalTransport

type Unit =
| Combat of Combat
| Transport of Transport

