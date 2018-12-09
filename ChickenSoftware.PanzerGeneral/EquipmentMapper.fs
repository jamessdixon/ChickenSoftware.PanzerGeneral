module ChickenSoftware.PanzerGeneral.EquipmentMapper

open Nation
open Equipment
open SupportData

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

let getTrackedEquipment (ed: EquipmentContext.Equipment) (motorizedEquipment: MotorizedEquipment) = 
    match ed.MovementTypeId with
    | 1 -> TrackedEquipment.HalfTrack (HalfTrackEquipment motorizedEquipment)
    | _ -> TrackedEquipment.FullTrack (FullTrackEquipment motorizedEquipment)

let getLandMotorizedEquipment (ed: EquipmentContext.Equipment) = 
    let motorizedEquipment = getMotorizedEquipment ed
    match ed.MovementTypeId with
    | 2 -> LandMotorizedEquipment.Wheeled (WheeledEquipment motorizedEquipment)
    | _ -> let trackedEquipment = getTrackedEquipment ed motorizedEquipment
           LandMotorizedEquipment.Tracked trackedEquipment

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
    {InfantryEquipment.BaseEquipment = equipment; 
    EntrenchableEquipment=entrenchableEquipment; 
    MoveableEquipment = moveableEquipment; 
    LandTargetCombatEquipment = combatEquipment}

let getTankEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let trackedEquipment = FullTrackEquipment motorizedEquipment
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {TankEquipment.BaseEquipment = equipment; 
    FullTrackedEquipment = trackedEquipment;
    LandTargetCombatEquipment = combatEquipment}

let getReconEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let landMotorizedEquipment = getLandMotorizedEquipment equipmentDatum
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {ReconEquipment.BaseEquipment = equipment; 
    LandMotorizedEquipment =  landMotorizedEquipment;
    LandTargetCombatEquipment = combatEquipment}

let getTankDestroyerEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let trackedEquipment = FullTrackEquipment motorizedEquipment
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {TankDestroyerEquipment.BaseEquipment = equipment; 
    FullTrackedEquipment =  trackedEquipment;
    LandTargetCombatEquipment = combatEquipment}

let getAntiAirEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let trackedEquipment = getTrackedEquipment equipmentDatum motorizedEquipment
    let combatEquipment = getAirTargetCombatEquipment equipmentDatum
    {AntiAirEquipment.BaseEquipment = equipment; 
    TrackedEquipment =  trackedEquipment;
    AirTargetCombatEquipment = combatEquipment}

let getEmplacementEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let landCombatEquipment = getLandTargetCombatEquipment equipmentDatum
    let airCombatEquipment = getAirTargetCombatEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {EmplacementEquipment.BaseEquipment = equipment;
    LandTargetCombatEquipment = landCombatEquipment;
    AirTargetCombatEquipment = airCombatEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getAntiTankEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let entrenchableEquipment = getEntrenchableEquipment equipmentDatum
    let moveableEquipment = getMovableEquipment equipmentDatum
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {AntiTankEquipment.BaseEquipment = equipment;
    EntrenchableEquipment =  entrenchableEquipment;
    MoveableEquipment = moveableEquipment; 
    LandTargetCombatEquipment = combatEquipment}

let getArtilleryEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let moveableEquipment = getMovableEquipment equipmentDatum
    let combatEquipment = getLandTargetCombatEquipment equipmentDatum
    {ArtilleryEquipment.BaseEquipment = equipment;
    MoveableEquipment = moveableEquipment; 
    LandTargetCombatEquipment = combatEquipment}

let getSelfPropelledArtilleryEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getArtilleryEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let trackedEquipment = getTrackedEquipment equipmentDatum motorizedEquipment
    {SelfPropelledArtilleryEquipment.ArtilleryEquipment = equipment;
    TrackedEquipment = trackedEquipment}

let getAirDefenseEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let moveableEquipment = getMovableEquipment equipmentDatum
    let combatEquipment = getAirTargetCombatEquipment equipmentDatum
    {AirDefenseEquipment.BaseEquipment = equipment;
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
    let trackedEquipment = getTrackedEquipment equipmentDatum motorizedEquipment
    {SelfPropelledAirDefenseEquipment.AirDefenseEquipment = equipment;
    TrackedEquipment= trackedEquipment}

let getAirFighterEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let landCombatEquipment = getLandTargetCombatEquipment equipmentDatum
    let airCombatEquipment = getAirTargetCombatEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {AirFighterEquipment.BaseEquipment = equipment;
    MotorizedEquipment =  motorizedEquipment;
    LandTargetCombatEquipment = landCombatEquipment;
    AirTargetCombatEquipment = airCombatEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getAirBomberEquipment (equipmentDatum: EquipmentContext.Equipment) = 
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let landCombatEquipment = getLandTargetCombatEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {AirBomberEquipment.BaseEquipment = equipment;
    MotorizedEquipment =  motorizedEquipment;
    LandTargetCombatEquipment = landCombatEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getSubmarineEquipment (equipmentDatum: EquipmentContext.Equipment) = 
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {SubmarineEquipment.BaseEquipment = equipment;
    MotorizedEquipment =  motorizedEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getSurfaceShipEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let landCombatEquipment = getLandTargetCombatEquipment equipmentDatum
    let airCombatEquipment = getAirTargetCombatEquipment equipmentDatum
    let navalCombatEquipment = getNavalTargetCombatEquipment equipmentDatum
    {SurfaceShipEquipment.BaseEquipment = equipment;
    MotorizedEquipment =  motorizedEquipment;
    LandTargetCombatEquipment = landCombatEquipment;
    AirTargetCombatEquipment = airCombatEquipment;
    NavalTargetCombatEquipment = navalCombatEquipment}

let getAircraftCarrierEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    {AircraftCarrierEquipment.BaseEquipment = equipment;
    MotorizedEquipment =  motorizedEquipment;}

let getLandTransportEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    let trackedEquipment = getTrackedEquipment equipmentDatum motorizedEquipment
    {LandTransportEquipment.BaseEquipment = equipment; 
    TrackedEquipment =  trackedEquipment}

let getAirTransportEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    {AirTransportEquipment.BaseEquipment = equipment; 
    MotorizedEquipment =  motorizedEquipment}

let getSeaTransportEquipment (equipmentDatum: EquipmentContext.Equipment) =
    let equipment = getBaseEquipment equipmentDatum
    let motorizedEquipment = getMotorizedEquipment equipmentDatum
    {SeaTransportEquipment.BaseEquipment = equipment; 
    MotorizedEquipment =  motorizedEquipment}

