module ChickenSoftware.PanzerGeneral.EquipmentMapper

open Nation
open Equipment
open EquipmentGenerator

let getBaseEquipment (ed: EquipmentContext.Equipment) =
    let nation = getNation ed.NationId 
    {Id = ed.EquipmentId; 
        Nation = nation;
        IconId = ed.Icon;
        Description = ed.EquipmentDescription;
        Cost = ed.Cost;
        YearAvailable = ed.Year;
        MonthAvailable = ed.Month;
        YearRetired = ed.LastYear;
        MonthRetired = ed.Month;
        MaximumSpottingRange = ed.Spotting;
        GroundDefensePoints = ed.GroundDefense;
        AirDefensePoints = ed.AirDefense;
        NavalDefensePoints = ed.GroundDefense;
        }

let getEntrenchableEquipment (equipmentDatum: EquipmentContext.Equipment) =
    {EntrenchmentRate = 1}

let getMovableEquipment (ed: EquipmentContext.Equipment) =
    {MaximumMovementPoints = ed.Movement}

let getMotorizedEquipment (ed: EquipmentContext.Equipment) =
    let moveableEquipment = {MaximumMovementPoints = ed.Movement}
    {MoveableEquipment = moveableEquipment; MaximumFuel = ed.MaxFuel}

let getCombatEquipment (ed: EquipmentContext.Equipment) =
    {MaximumAmmo = ed.MaxAmmo; Initative = ed.Initiative}

let getLandTargetCombatEquipment (ed: EquipmentContext.Equipment) =
    let combatEquipment = {MaximumAmmo = ed.MaxAmmo; Initative = ed.Initiative}
    {CombatEquipment=combatEquipment; HardAttackPoints = ed.HardAttack; SoftAttackPoints = ed.SoftAttack}

let getAirTargetCombatEquipment (ed: EquipmentContext.Equipment) =
    let combatEquipment = {MaximumAmmo = ed.MaxAmmo; Initative = ed.Initiative}
    {CombatEquipment=combatEquipment; AirAttackPoints = ed.AirAttack}

let getNavalTargetCombatEquipment (ed: EquipmentContext.Equipment) =
    let combatEquipment = {MaximumAmmo = ed.MaxAmmo; Initative = ed.Initiative}
    {CombatEquipment=combatEquipment; NavalAttackPoints = ed.NavalAttack}

let getInfantryEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let entrenchableEquipment = getEntrenchableEquipment equipmentDatum
    let moveableEquipment = getMovableEquipment equipmentDatum
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {InfantryEquipment.Equipment = equipment; 
    EntrenchableEquipment=entrenchableEquipment; 
    MoveableEquipment = moveableEquipment; 
    LandTargetCombatEquipment = combatEquipment}

let getTankEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {TankEquipment.Equipment = equipment; 
    MotorizedEquipment =  motorizedEquipment;
    LandTargetCombatEquipment = combatEquipment}

let getReconEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {ReconEquipment.Equipment = equipment; 
    MotorizedEquipment =  motorizedEquipment;
    LandTargetCombatEquipment = combatEquipment}

let getTankDestroyerEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {TankDestroyerEquipment.Equipment = equipment; 
    MotorizedEquipment =  motorizedEquipment;
    LandTargetCombatEquipment = combatEquipment}

let getAntiAirEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let entrenchableEquipment = getEntrenchableEquipment equipmentDatum
    let combatEquipment = getAirTargetCombatEquipment equipmentDatum
    {AntiAirEquipment.Equipment = equipment; 
    EntrenchableEquipment =  entrenchableEquipment;
    AirTargetCombatEquipment = combatEquipment}

let getEmplacementEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let landCombatEquipment = getLandTargetCombatEquipment equipmentDatum
    let airCombatEquipment = getAirTargetCombatEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {EmplacementEquipment.Equipment = equipment;
    LandTargetCombatEquipment = landCombatEquipment;
    AirTargetCombatEquipment = airCombatEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getAntiTankEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let entrenchableEquipment = getEntrenchableEquipment equipmentDatum
    let moveableEquipment = getMovableEquipment equipmentDatum
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {AntiTankEquipment.Equipment = equipment;
    EntrenchableEquipment =  entrenchableEquipment;
    MoveableEquipment = moveableEquipment; 
    LandTargetCombatEquipment = combatEquipment}

let getArtilleryEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let moveableEquipment = getMovableEquipment equipmentDatum
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {ArtilleryEquipment.Equipment = equipment;
    MoveableEquipment = moveableEquipment; 
    LandTargetCombatEquipment = combatEquipment}

let getSelfPropelledArtilleryEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getArtilleryEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    {SelfPropelledArtilleryEquipment.ArtilleryEquipment = equipment;
    MotorizedEquipment = motorizedEquipment}

let getAirDefenseEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let moveableEquipment = getMovableEquipment equipmentDatum
    let combatEquipment = getAirTargetCombatEquipment equipmentDatum
    {AirDefenseEquipment.Equipment = equipment;
    MoveableEquipment = moveableEquipment; 
    AirTargetCombatEquipment = combatEquipment}

let getTowedAirDefenseEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getAirDefenseEquipment equipmentDatum
    let entrenchableEquipment = getEntrenchableEquipment equipmentDatum
    {TowedAirDefenseEquipment.AirDefenseEquipment = equipment;
    EntrenchableEquipment= entrenchableEquipment}

let getSelfPropelledAirDefenseEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getAirDefenseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    {SelfPropelledAirDefenseEquipment.AirDefenseEquipment = equipment;
    MotorizedEquipment= motorizedEquipment}

let getAirFighterEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let landCombatEquipment = getLandTargetCombatEquipment equipmentDatum
    let airCombatEquipment = getAirTargetCombatEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {AirFighterEquipment.Equipment = equipment;
    MotorizedEquipment =  motorizedEquipment;
    LandTargetCombatEquipment = landCombatEquipment;
    AirTargetCombatEquipment = airCombatEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getAirBomberEquipment (equipmentDatum: EquipmentContext.Equipment) = 
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let landCombatEquipment = getLandTargetCombatEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {AirBomberEquipment.Equipment = equipment;
    MotorizedEquipment =  motorizedEquipment;
    LandTargetCombatEquipment = landCombatEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getSubmarineEquipment (equipmentDatum: EquipmentContext.Equipment) = 
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {SubmarineEquipment.Equipment = equipment;
    MotorizedEquipment =  motorizedEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getSurfaceShipEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let landCombatEquipment = getLandTargetCombatEquipment equipmentDatum
    let airCombatEquipment = getAirTargetCombatEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {SurfaceShipEquipment.Equipment = equipment;
    MotorizedEquipment =  motorizedEquipment;
    LandTargetCombatEquipment = landCombatEquipment;
    AirTargetCombatEquipment = airCombatEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getAircraftCarrierEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    {AircraftCarrierEquipment.Equipment = equipment;
    MotorizedEquipment =  motorizedEquipment;}

let getTransportEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    {TransportEquipment.Equipment = equipment; 
    MotorizedEquipment =  motorizedEquipment}

