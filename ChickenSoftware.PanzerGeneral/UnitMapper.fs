module ChickenSoftware.PanzerGeneral.UnitMapper

open Unit
open Nation
open Equipment
open UnitGenerator
open EquipmentMapper
open EquipmentGenerator

let getReinforcementType id =
    match id with 
    | 0 -> ReinforcementType.Core
    | _ -> ReinforcementType.Auxiliary
    
let getUnitStats (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment)=
    let unitName = equipmentDatum.EquipmentDescription + " - " + id.ToString()
    {UnitStats.Id=id; Name = unitName; Strength=su.Strength}

let getCombatStats (su: ScenarioUnitContext.ScenarioUnit) =
    let reinforcementType = getReinforcementType su.AuxiliaryInd
    {CombatStats.Ammo=su.Ammo; Experience=su.Experience; ReinforcementType=reinforcementType}

let getBridgingUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let equipment = getInfantryEquipment equipmentDatum
    let unit = {UnitStats = unitStats; CombatStats= combatStats;Equipment = equipment; CanBridge = true; CanParaDrop = false}
    let infantry = Infantry.Bridging unit
    let landCombat = LandCombat.Infantry infantry
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getAirborneUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let equipment = getInfantryEquipment equipmentDatum
    let unit = {UnitStats = unitStats; CombatStats= combatStats;Equipment = equipment; CanBridge = false; CanParaDrop = true}
    let infantry = Infantry.Airborne unit
    let landCombat = LandCombat.Infantry infantry
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getBasicInfantryUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let equipment = getInfantryEquipment equipmentDatum
    let unit = {UnitStats = unitStats; CombatStats= combatStats;Equipment = equipment; CanBridge = false; CanParaDrop = false}
    let infantry = Infantry.Basic unit
    let landCombat = LandCombat.Infantry infantry
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getEngeneerUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let equipment = getInfantryEquipment equipmentDatum
    let unit = {UnitStats = unitStats; CombatStats= combatStats;Equipment = equipment; CanBridge = false; CanParaDrop = false}
    let infantry = Infantry.Engineer unit
    let landCombat = LandCombat.Infantry infantry
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getTankUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment = getTankEquipment equipmentDatum
    let unit = {TankUnit.UnitStats=unitStats;CombatStats= combatStats; MotorizedMovementStats=motorizedMovementStats;Equipment=equipment}
    let landCombat = LandCombat.Tank unit
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getTankDestroyerUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment = getTankDestroyerEquipment equipmentDatum
    let unit = {TankDestroyerUnit.UnitStats=unitStats;CombatStats= combatStats; MotorizedMovementStats=motorizedMovementStats;Equipment=equipment}
    let landCombat = LandCombat.TankDestroyer unit
    let combat = Combat.Land landCombat
    Unit.Combat combat
    
let getReconUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment = getReconEquipment equipmentDatum
    let unit = {ReconUnit.UnitStats=unitStats;CombatStats= combatStats; MotorizedMovementStats=motorizedMovementStats;Equipment=equipment}
    let landCombat = LandCombat.Recon unit
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getTowedLightAntiTankUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let equipment = getAntiTankEquipment equipmentDatum
    let unit = {AntiTankUnit.UnitStats = unitStats; CombatStats= combatStats; Equipment = equipment;}
    let antiTank = AntiTank.Light unit
    let landCombat = LandCombat.AntiTank antiTank
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getTowedLightArtilleryUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let equipment = getArtilleryEquipment equipmentDatum
    let unit = {TowedArtilleryUnit.UnitStats = unitStats; CombatStats= combatStats; Equipment = equipment;}
    let lightArtillery = TowedArtillery.Light unit
    let atrillery = Artillery.Towed lightArtillery  
    let landCombat = LandCombat.Artillery atrillery
    let combat = Combat.Land landCombat
    Unit.Combat combat
    
let getTowedHeavyAntiTankUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let equipment = getAntiTankEquipment equipmentDatum
    let unit = {AntiTankUnit.UnitStats = unitStats; CombatStats= combatStats; Equipment = equipment;}
    let antiTank = AntiTank.Heavy unit
    let landCombat = LandCombat.AntiTank antiTank
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getTowedHeavyArtilleryUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let equipment = getArtilleryEquipment equipmentDatum
    let unit = {TowedArtilleryUnit.UnitStats = unitStats; CombatStats= combatStats; Equipment = equipment;}
    let heavyArtillery = TowedArtillery.Heavy unit
    let atrillery = Artillery.Towed heavyArtillery  
    let landCombat = LandCombat.Artillery atrillery
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getSelfPropelledArtilleryUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment = getArtilleryEquipment equipmentDatum
    let unit = {SelfPropelledArtilleryUnit.UnitStats = unitStats; CombatStats= combatStats; MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let atrillery = Artillery.SelfPropelled unit  
    let landCombat = LandCombat.Artillery atrillery
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getAntiAirUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment = getAntiAirEquipment equipmentDatum
    let unit = {AntiAirUnit.UnitStats = unitStats; CombatStats= combatStats; MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let landCombat = LandCombat.AntiAir unit
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getTowedAirDefenseUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let equipment = getTowedAirDefenseEquipment equipmentDatum
    let unit = {TowedAirDefenseUnit.UnitStats = unitStats; CombatStats= combatStats;Equipment = equipment;}
    let airDefense = AirDefense.Towed unit
    let landCombat = LandCombat.AirDefense airDefense
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getSelfPropelledAirDefenseUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment = getSelfPropelledAirDefenseEquipment equipmentDatum
    let unit = {SelfPropelledAirDefenseUnit.UnitStats = unitStats; CombatStats= combatStats; 
        MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let airDefense = AirDefense.SelfPropelled unit
    let landCombat = LandCombat.AirDefense airDefense
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getFortUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let equipment = getEmplacementEquipment equipmentDatum
    let combatStats = getCombatStats su
    let unit = {EmplacementUnit.UnitStats = unitStats; CombatStats= combatStats; Equipment = equipment;}
    let emplacement = Emplacement.Fort unit
    let landCombat = LandCombat.Emplacement emplacement
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getStrongPointUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let equipment = getEmplacementEquipment equipmentDatum
    let combatStats = getCombatStats su
    let unit = {EmplacementUnit.UnitStats = unitStats; CombatStats= combatStats; Equipment = equipment;}
    let emplacement = Emplacement.Strongpoint unit
    let landCombat = LandCombat.Emplacement emplacement
    let combat = Combat.Land landCombat
    Unit.Combat combat

let getPropFighterUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment = getAirFighterEquipment equipmentDatum
    let unit = {AirFighterUnit.UnitStats = unitStats; CombatStats= combatStats; MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let fighter = Fighter.Prop unit
    let airCombat = AirCombat.Fighter fighter
    let combat = Combat.Air airCombat
    Unit.Combat combat

let getJetFighterUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment = getAirFighterEquipment equipmentDatum
    let unit = {AirFighterUnit.UnitStats = unitStats; CombatStats= combatStats; MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let fighter = Fighter.Jet unit
    let airCombat = AirCombat.Fighter fighter
    let combat = Combat.Air airCombat
    Unit.Combat combat

let getTacticalBomberUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getAirBomberEquipment equipmentDatum
    let unit = {AirBomberUnit.UnitStats = unitStats; CombatStats= combatStats; MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let bomber = Bomber.Tactical unit
    let airCombat = AirCombat.Bomber bomber
    let combat = Combat.Air airCombat
    Unit.Combat combat

let getStrategicBomberUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getAirBomberEquipment equipmentDatum
    let unit = {AirBomberUnit.UnitStats = unitStats; CombatStats= combatStats; MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let bomber = Bomber.Strategic unit
    let airCombat = AirCombat.Bomber bomber
    let combat = Combat.Air airCombat
    Unit.Combat combat

let getSubmarineUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getSubmarineEquipment equipmentDatum
    let unit = {SubmarineUnit.UnitStats = unitStats; CombatStats= combatStats; MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let navalCombat = NavalCombat.Submarine unit
    let combat = Combat.Naval navalCombat
    Unit.Combat combat

let getDestroyerUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getSurfaceShipEquipment equipmentDatum
    let unit = {SurfaceShipEquipmentUnit.UnitStats = unitStats; CombatStats= combatStats; MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let navalCombat = NavalCombat.Destroyer unit
    let combat = Combat.Naval navalCombat
    Unit.Combat combat

let getCapitalShipUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let combatStats = getCombatStats su
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getSurfaceShipEquipment equipmentDatum
    let unit = {SurfaceShipEquipmentUnit.UnitStats = unitStats; CombatStats= combatStats; MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
    let navalCombat = NavalCombat.CapitalShip unit
    let combat = Combat.Naval navalCombat
    Unit.Combat combat

let getAircraftCarrierUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getTransportEquipment equipmentDatum
    let transport = {TransportUnit.UnitStats=unitStats;MotorizedMovementStats= motorizedMovementStats;Equipment=equipment}
    let unit = {AircraftCarrierUnit.TranportUnit=transport;Payload=None}
    let navalTransport = NavalTransport.AircraftCarrier unit
    let transport = Transport.Naval navalTransport
    Unit.Transport transport

let getLandingCraftUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getTransportEquipment equipmentDatum
    let transport = {TransportUnit.UnitStats=unitStats;MotorizedMovementStats= motorizedMovementStats;Equipment=equipment}
    let unit = {LandingCraftUnit.TransportUnit=transport;Payload=None}
    let navalTransport = NavalTransport.LandingCraft unit
    let transport = Transport.Naval navalTransport
    Unit.Transport transport
    
let getLandTransportUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getTransportEquipment equipmentDatum
    let transport = {TransportUnit.UnitStats=unitStats;MotorizedMovementStats= motorizedMovementStats;Equipment=equipment}
    let unit = {LandTransportUnit.TransportUnit=transport;Payload=None}
    let transport = Transport.Land unit
    Unit.Transport transport

let getAirTransportUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getTransportEquipment equipmentDatum
    let transport = {TransportUnit.UnitStats=unitStats;MotorizedMovementStats= motorizedMovementStats;Equipment=equipment}
    let unit = {AirTransportUnit.TransportUnit=transport;Payload=None}
    let transport = Transport.Air unit
    Unit.Transport transport

let getUnitFromId subclassId =
    match subclassId with
    | 0 -> getBasicInfantryUnit
    | 1 -> getTankUnit
    | 2 -> getReconUnit
    | 3 -> getTowedLightAntiTankUnit
    | 4 -> getTowedLightArtilleryUnit
    | 5 -> getAntiAirUnit
    | 6 -> getTowedAirDefenseUnit
    | 7 -> getFortUnit
    | 8 -> getPropFighterUnit
    | 9 -> getTacticalBomberUnit
    | 10 -> getStrategicBomberUnit
    | 11 -> getSubmarineUnit
    | 12 -> getDestroyerUnit
    | 13 -> getCapitalShipUnit
    | 14 -> getAircraftCarrierUnit
    | 15 -> getLandTransportUnit
    | 16 -> getAirTransportUnit
    | 17 -> getLandingCraftUnit
    | 18 -> getTankDestroyerUnit
    | 19 -> getSelfPropelledArtilleryUnit
    | 20 -> getSelfPropelledAirDefenseUnit
    | 21 -> getJetFighterUnit
    | 22 -> getEngeneerUnit
    | 23 -> getAirborneUnit
    | 24 -> getBridgingUnit
    | 25 -> getStrongPointUnit
    | 26 -> getTowedHeavyAntiTankUnit
    | 27 -> getTowedHeavyArtilleryUnit
    | _ -> getBasicInfantryUnit

let getUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentData: EquipmentContext.Equipment array) =
    let equipmentDatum = equipmentData |> Array.tryFind(fun ed -> ed.EquipmentId = su.EquipmentId)
    match equipmentDatum with
    | Some ed -> Some (getUnitFromId ed.EquipmentSubClassId id su ed)
    | None -> None

let getBaseEquipment unit =
    match unit with 
    | Unit.Combat c -> 
        match c with 
        | Combat.Air ac -> 
            match ac with
            | AirCombat.Fighter acf ->
                match  acf with
                | Fighter.Prop acfp -> acfp.Equipment.Equipment
                | Fighter.Jet acfj -> acfj.Equipment.Equipment
            | AirCombat.Bomber acb ->
                match acb with
                | Bomber.Tactical acbt -> acbt.Equipment.Equipment
                | Bomber.Strategic acbs -> acbs.Equipment.Equipment
        | Combat.Land lc ->
            match lc with 
            | LandCombat.AirDefense lcad ->
                match lcad with 
                | AirDefense.SelfPropelled lcadsp -> lcadsp.Equipment.Equipment
                | AirDefense.Towed lcadt -> lcadt.Equipment.AirDefenseEquipment.Equipment 
            | LandCombat.AntiAir lcaa -> lcaa.Equipment.Equipment
            | LandCombat.AntiTank lcat ->
                match lcat with
                | AntiTank.Light lcatl -> lcatl.Equipment.Equipment
                | AntiTank.Heavy lcath -> lcath.Equipment.Equipment
            | LandCombat.Artillery lca ->
                match lca with
                | Artillery.Towed lcat ->
                    match lcat with 
                    | TowedArtillery.Light lcatl -> lcatl.Equipment.Equipment
                    | TowedArtillery.Heavy lcath -> lcath.Equipment.Equipment
                | Artillery.SelfPropelled lcasp -> lcasp.Equipment.Equipment    
            | LandCombat.Emplacement lce -> 
                match lce with
                | Emplacement.Fort lcef -> lcef.Equipment.Equipment
                | Emplacement.Strongpoint lces -> lces.Equipment.Equipment
            | LandCombat.Infantry lci ->
                match lci with 
                | Infantry.Airborne lcia -> lcia.Equipment.Equipment
                | Infantry.Basic lcib -> lcib.Equipment.Equipment
                | Infantry.Bridging lcig -> lcig.Equipment.Equipment
                | Infantry.Engineer lcie -> lcie.Equipment.Equipment
                | Infantry.HeavyWeapon lcihw -> lcihw.Equipment.Equipment
                | Infantry.Ranger lcir -> lcir.Equipment.Equipment
            | LandCombat.Recon lcr -> lcr.Equipment.Equipment
            | LandCombat.Tank lct -> lct.Equipment.Equipment
            | LandCombat.TankDestroyer lctd -> lctd.Equipment.Equipment
        | Combat.Naval nc ->
            match nc with
            | NavalCombat.CapitalShip nccs -> nccs.Equipment.Equipment 
            | NavalCombat.Destroyer ncd -> ncd.Equipment.Equipment
            | NavalCombat.Submarine ncs -> ncs.Equipment.Equipment
    | Unit.Transport t -> 
        match t with 
        | Transport.Air at -> at.TransportUnit.Equipment.Equipment
        | Transport.Land lt -> lt.TransportUnit.Equipment.Equipment
        | Transport.Naval nt -> 
            match nt with
            | NavalTransport.AircraftCarrier ntac -> ntac.TranportUnit.Equipment.Equipment
            | NavalTransport.LandingCraft ntlc -> ntlc.TransportUnit.Equipment.Equipment

let getUnitIconId unit =
    let equipment = getBaseEquipment unit
    equipment.IconId

let getMovementPoints unit =
    match unit with 
    | Unit.Combat c -> 
        match c with 
        | Combat.Air ac -> 
            match ac with
            | AirCombat.Fighter acf ->
                match  acf with
                | Fighter.Prop acfp -> acfp.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
                | Fighter.Jet acfj -> acfj.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
            | AirCombat.Bomber acb ->
                match acb with
                | Bomber.Tactical acbt -> acbt.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
                | Bomber.Strategic acbs -> acbs.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
        | Combat.Land lc ->
            match lc with 
            | LandCombat.AirDefense lcad ->
                match lcad with 
                | AirDefense.SelfPropelled lcadsp -> lcadsp.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
                | AirDefense.Towed lcadt -> lcadt.Equipment.AirDefenseEquipment.MoveableEquipment.MaximumMovementPoints
            | LandCombat.AntiAir lcaa -> 1
            | LandCombat.AntiTank lcat ->
                match lcat with
                | AntiTank.Light lcatl -> lcatl.Equipment.MoveableEquipment.MaximumMovementPoints
                | AntiTank.Heavy lcath -> lcath.Equipment.MoveableEquipment.MaximumMovementPoints
            | LandCombat.Artillery lca ->
                match lca with
                | Artillery.Towed lcat ->
                    match lcat with 
                    | TowedArtillery.Light lcatl -> lcatl.Equipment.MoveableEquipment.MaximumMovementPoints
                    | TowedArtillery.Heavy lcath -> lcath.Equipment.MoveableEquipment.MaximumMovementPoints
                | Artillery.SelfPropelled lcasp -> lcasp.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
            | LandCombat.Emplacement lce -> 
                match lce with
                | Emplacement.Fort lcef -> 0
                | Emplacement.Strongpoint lces -> 0
            | LandCombat.Infantry lci ->
                match lci with 
                | Infantry.Airborne lcia -> lcia.Equipment.MoveableEquipment.MaximumMovementPoints
                | Infantry.Basic lcib -> lcib.Equipment.MoveableEquipment.MaximumMovementPoints
                | Infantry.Bridging lcig -> lcig.Equipment.MoveableEquipment.MaximumMovementPoints
                | Infantry.Engineer lcie -> lcie.Equipment.MoveableEquipment.MaximumMovementPoints
                | Infantry.HeavyWeapon lcihw -> lcihw.Equipment.MoveableEquipment.MaximumMovementPoints
                | Infantry.Ranger lcir -> lcir.Equipment.MoveableEquipment.MaximumMovementPoints
            | LandCombat.Recon lcr -> lcr.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
            | LandCombat.Tank lct -> lct.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
            | LandCombat.TankDestroyer lctd -> lctd.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
        | Combat.Naval nc ->
            match nc with
            | NavalCombat.CapitalShip nccs -> nccs.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints 
            | NavalCombat.Destroyer ncd -> ncd.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
            | NavalCombat.Submarine ncs -> ncs.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
    | Unit.Transport t -> 
        match t with 
        | Transport.Air at -> at.TransportUnit.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
        | Transport.Land lt -> lt.TransportUnit.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
        | Transport.Naval nt -> 
            match nt with
            | NavalTransport.AircraftCarrier ntac -> ntac.TranportUnit.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
            | NavalTransport.LandingCraft ntlc -> ntlc.TransportUnit.Equipment.MotorizedEquipment.MoveableEquipment.MaximumMovementPoints
