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
    let equipment = getSelfPropelledArtilleryEquipment equipmentDatum
    let unit = {SelfPropelledArtilleryUnit.UnitStats = unitStats; CombatStats= combatStats; 
        MotorizedMovementStats= motorizedMovementStats;Equipment = equipment;}
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
    let equipment =  getSeaTransportEquipment equipmentDatum
    let transport = {SeaTransportUnit.UnitStats=unitStats;MotorizedMovementStats= motorizedMovementStats;Equipment=equipment}
    let unit = {AircraftCarrierUnit.TranportUnit=transport;Payload=None}
    let navalTransport = NavalTransport.AircraftCarrier unit
    let transport = Transport.Naval navalTransport
    Unit.Transport transport

let getLandingCraftUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getSeaTransportEquipment equipmentDatum
    let transport = {SeaTransportUnit.UnitStats=unitStats;MotorizedMovementStats= motorizedMovementStats;Equipment=equipment}
    let unit = {LandingCraftUnit.TransportUnit=transport;Payload=None}
    let navalTransport = NavalTransport.LandingCraft unit
    let transport = Transport.Naval navalTransport
    Unit.Transport transport
    
let getLandTransportUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getLandTransportEquipment equipmentDatum
    let unit = {LandTransportUnit.UnitStats=unitStats;MotorizedMovementStats= motorizedMovementStats;Equipment=equipment;Payload=None}
    let transport = Transport.Land unit
    Unit.Transport transport

let getAirTransportUnit (id:int) (su: ScenarioUnitContext.ScenarioUnit) (equipmentDatum: EquipmentContext.Equipment) =
    let unitStats = getUnitStats id su equipmentDatum
    let motorizedMovementStats = {Fuel = equipmentDatum.MaxFuel}
    let equipment =  getAirTransportEquipment equipmentDatum
    let unit = {AirTransportUnit.UnitStats=unitStats;MotorizedMovementStats= motorizedMovementStats;Equipment=equipment;Payload=None}
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
                | Fighter.Prop acfp -> acfp.Equipment.BaseEquipment
                | Fighter.Jet acfj -> acfj.Equipment.BaseEquipment
            | AirCombat.Bomber acb ->
                match acb with
                | Bomber.Tactical acbt -> acbt.Equipment.BaseEquipment
                | Bomber.Strategic acbs -> acbs.Equipment.BaseEquipment
        | Combat.Land lc ->
            match lc with 
            | LandCombat.AirDefense lcad ->
                match lcad with 
                | AirDefense.SelfPropelled lcadsp -> lcadsp.Equipment.AirDefenseEquipment.BaseEquipment
                | AirDefense.Towed lcadt -> lcadt.Equipment.AirDefenseEquipment.BaseEquipment 
            | LandCombat.AntiAir lcaa -> lcaa.Equipment.BaseEquipment
            | LandCombat.AntiTank lcat ->
                match lcat with
                | AntiTank.Light lcatl -> lcatl.Equipment.BaseEquipment
                | AntiTank.Heavy lcath -> lcath.Equipment.BaseEquipment
            | LandCombat.Artillery lca ->
                match lca with
                | Artillery.Towed lcat ->
                    match lcat with 
                    | TowedArtillery.Light lcatl -> lcatl.Equipment.BaseEquipment
                    | TowedArtillery.Heavy lcath -> lcath.Equipment.BaseEquipment
                | Artillery.SelfPropelled lcasp -> lcasp.Equipment.ArtilleryEquipment.BaseEquipment  
            | LandCombat.Emplacement lce -> 
                match lce with
                | Emplacement.Fort lcef -> lcef.Equipment.BaseEquipment
                | Emplacement.Strongpoint lces -> lces.Equipment.BaseEquipment
            | LandCombat.Infantry lci ->
                match lci with 
                | Infantry.Airborne lcia -> lcia.Equipment.BaseEquipment
                | Infantry.Basic lcib -> lcib.Equipment.BaseEquipment
                | Infantry.Bridging lcig -> lcig.Equipment.BaseEquipment
                | Infantry.Engineer lcie -> lcie.Equipment.BaseEquipment
                | Infantry.HeavyWeapon lcihw -> lcihw.Equipment.BaseEquipment
                | Infantry.Ranger lcir -> lcir.Equipment.BaseEquipment
            | LandCombat.Recon lcr -> lcr.Equipment.BaseEquipment
            | LandCombat.Tank lct -> lct.Equipment.BaseEquipment
            | LandCombat.TankDestroyer lctd -> lctd.Equipment.BaseEquipment
        | Combat.Naval nc ->
            match nc with
            | NavalCombat.CapitalShip nccs -> nccs.Equipment.BaseEquipment 
            | NavalCombat.Destroyer ncd -> ncd.Equipment.BaseEquipment
            | NavalCombat.Submarine ncs -> ncs.Equipment.BaseEquipment
    | Unit.Transport t -> 
        match t with 
        | Transport.Air at -> at.Equipment.BaseEquipment
        | Transport.Land lt -> lt.Equipment.BaseEquipment
        | Transport.Naval nt -> 
            match nt with
            | NavalTransport.AircraftCarrier ntac -> ntac.TranportUnit.Equipment.BaseEquipment
            | NavalTransport.LandingCraft ntlc -> ntlc.TransportUnit.Equipment.BaseEquipment

let getMoveableEquipment unit =
    match unit with 
    | Unit.Combat c -> 
        match c with 
        | Combat.Air ac -> 
            match ac with
            | AirCombat.Fighter acf ->
                match  acf with
                | Fighter.Prop acfp -> Some acfp.Equipment.MotorizedEquipment.MoveableEquipment
                | Fighter.Jet acfj -> Some acfj.Equipment.MotorizedEquipment.MoveableEquipment
            | AirCombat.Bomber acb ->
                match acb with
                | Bomber.Tactical acbt -> Some acbt.Equipment.MotorizedEquipment.MoveableEquipment
                | Bomber.Strategic acbs -> Some acbs.Equipment.MotorizedEquipment.MoveableEquipment
        | Combat.Land lc ->
            match lc with 
            | LandCombat.AirDefense lcad ->
                match lcad with 
                | AirDefense.SelfPropelled lcadsp -> Some lcadsp.Equipment.AirDefenseEquipment.MoveableEquipment
                | AirDefense.Towed lcadt -> Some lcadt.Equipment.AirDefenseEquipment.MoveableEquipment 
            | LandCombat.AntiAir lcaa -> 
                let trackedEquipment = lcaa.Equipment.TrackedEquipment
                match trackedEquipment with
                | TrackedEquipment.FullTrack lcaaft -> 
                    match lcaaft with
                    | FullTrackEquipment fte -> Some fte.MoveableEquipment
                | TrackedEquipment.HalfTrack lcaaht ->
                    match lcaaht with
                    | HalfTrackEquipment fte -> Some fte.MoveableEquipment
            | LandCombat.AntiTank lcat ->
                match lcat with
                | AntiTank.Light lcatl -> Some lcatl.Equipment.MoveableEquipment
                | AntiTank.Heavy lcath -> Some lcath.Equipment.MoveableEquipment
            | LandCombat.Artillery lca ->
                match lca with
                | Artillery.Towed lcat ->
                    match lcat with 
                    | TowedArtillery.Light lcatl -> Some lcatl.Equipment.MoveableEquipment
                    | TowedArtillery.Heavy lcath -> Some lcath.Equipment.MoveableEquipment
                | Artillery.SelfPropelled lcasp -> 
                    match lcasp.Equipment.TrackedEquipment with
                    | TrackedEquipment.FullTrack lcaaft -> 
                        match lcaaft with | FullTrackEquipment fte -> Some fte.MoveableEquipment
                    | TrackedEquipment.HalfTrack lcaaht ->
                        match lcaaht with | HalfTrackEquipment fte -> Some fte.MoveableEquipment
            | LandCombat.Emplacement lce -> 
                match lce with
                | Emplacement.Fort lcef -> None
                | Emplacement.Strongpoint lces -> None
            | LandCombat.Infantry lci ->
                match lci with 
                | Infantry.Airborne lcia -> Some lcia.Equipment.MoveableEquipment
                | Infantry.Basic lcib -> Some lcib.Equipment.MoveableEquipment
                | Infantry.Bridging lcig -> Some lcig.Equipment.MoveableEquipment
                | Infantry.Engineer lcie -> Some lcie.Equipment.MoveableEquipment
                | Infantry.HeavyWeapon lcihw -> Some lcihw.Equipment.MoveableEquipment
                | Infantry.Ranger lcir -> Some lcir.Equipment.MoveableEquipment
            | LandCombat.Recon lcr -> 
                match lcr.Equipment.LandMotorizedEquipment with
                | LandMotorizedEquipment.Tracked lcrt ->
                    match lcrt with
                    | FullTrack lcrtft -> match lcrtft with | FullTrackEquipment fte -> Some fte.MoveableEquipment
                    | HalfTrack lcrtht -> match lcrtht with | HalfTrackEquipment hte -> Some hte.MoveableEquipment
                | LandMotorizedEquipment.Wheeled lcrw -> 
                    match lcrw with | WheeledEquipment we -> Some we.MoveableEquipment
            | LandCombat.Tank lct -> 
                match lct.Equipment.FullTrackedEquipment with | FullTrackEquipment fte -> Some fte.MoveableEquipment
            | LandCombat.TankDestroyer lctd -> 
                match lctd.Equipment.FullTrackedEquipment with| FullTrackEquipment fte -> Some fte.MoveableEquipment
        | Combat.Naval nc ->
            match nc with
            | NavalCombat.CapitalShip nccs -> Some nccs.Equipment.MotorizedEquipment.MoveableEquipment 
            | NavalCombat.Destroyer ncd -> Some ncd.Equipment.MotorizedEquipment.MoveableEquipment
            | NavalCombat.Submarine ncs -> Some ncs.Equipment.MotorizedEquipment.MoveableEquipment
    | Unit.Transport t -> 
        match t with 
        | Transport.Air at -> match at with | atu -> Some atu.Equipment.MotorizedEquipment.MoveableEquipment
        | Transport.Land lt -> 
            match lt with 
            | ltu -> 
                match ltu.Equipment.TrackedEquipment with
                | FullTrack lcrtft -> match lcrtft with | FullTrackEquipment fte -> Some fte.MoveableEquipment
                | HalfTrack lcrtht -> match lcrtht with | HalfTrackEquipment hte -> Some hte.MoveableEquipment
        | Transport.Naval nt -> 
            match nt with
            | NavalTransport.AircraftCarrier ntac -> Some ntac.TranportUnit.Equipment.MotorizedEquipment.MoveableEquipment
            | NavalTransport.LandingCraft ntlc -> Some ntlc.TransportUnit.Equipment.MotorizedEquipment.MoveableEquipment

let getMotorizedMovementStats unit =
    match unit with 
    | Unit.Combat c -> 
        match c with 
        | Combat.Air ac -> 
            match ac with
            | AirCombat.Fighter acf ->
                match  acf with
                | Fighter.Prop acfp -> Some acfp.MotorizedMovementStats
                | Fighter.Jet acfj -> Some acfj.MotorizedMovementStats
            | AirCombat.Bomber acb ->
                match acb with
                | Bomber.Tactical acbt -> Some acbt.MotorizedMovementStats
                | Bomber.Strategic acbs -> Some acbs.MotorizedMovementStats
        | Combat.Land lc ->
            match lc with 
            | LandCombat.AirDefense lcad ->
                match lcad with 
                | AirDefense.SelfPropelled lcadsp -> Some lcadsp.MotorizedMovementStats
                | AirDefense.Towed lcadt -> None
            | LandCombat.AntiAir lcaa -> Some lcaa.MotorizedMovementStats
            | LandCombat.AntiTank lcat ->
                match lcat with
                | AntiTank.Light lcatl -> None
                | AntiTank.Heavy lcath -> None
            | LandCombat.Artillery lca ->
                match lca with
                | Artillery.Towed lcat ->
                    match lcat with 
                    | TowedArtillery.Light lcatl -> None
                    | TowedArtillery.Heavy lcath -> None
                | Artillery.SelfPropelled lcasp -> Some lcasp.MotorizedMovementStats
            | LandCombat.Emplacement lce -> 
                match lce with
                | Emplacement.Fort lcef -> None
                | Emplacement.Strongpoint lces -> None
            | LandCombat.Infantry lci ->
                match lci with 
                | Infantry.Airborne lcia -> None
                | Infantry.Basic lcib -> None
                | Infantry.Bridging lcig -> None
                | Infantry.Engineer lcie -> None
                | Infantry.HeavyWeapon lcihw -> None
                | Infantry.Ranger lcir -> None
            | LandCombat.Recon lcr -> Some lcr.MotorizedMovementStats
            | LandCombat.Tank lct -> Some lct.MotorizedMovementStats
            | LandCombat.TankDestroyer lctd -> Some lctd.MotorizedMovementStats
        | Combat.Naval nc ->
            match nc with
            | NavalCombat.CapitalShip nccs -> Some nccs.MotorizedMovementStats 
            | NavalCombat.Destroyer ncd -> Some ncd.MotorizedMovementStats
            | NavalCombat.Submarine ncs -> Some ncs.MotorizedMovementStats
    | Unit.Transport t -> 
        match t with 
        | Transport.Air at -> None
        | Transport.Land lt -> None
        | Transport.Naval nt -> 
            match nt with
            | NavalTransport.AircraftCarrier ntac -> Some ntac.TranportUnit.MotorizedMovementStats
            | NavalTransport.LandingCraft ntlc -> Some ntlc.TransportUnit.MotorizedMovementStats

let getUnitIconId unit =
    let equipment = getBaseEquipment unit
    equipment.IconId

let getUnitMovementPoints (unit:Unit) =
    let moveableEquipment = getMoveableEquipment unit
    let motorizedMovementStats = getMotorizedMovementStats unit
    match moveableEquipment, motorizedMovementStats with 
    | Some mv, Some mms -> 
        match mv.MaximumMovementPoints > mms.Fuel with
        | true -> mms.Fuel
        | false -> mv.MaximumMovementPoints
    | Some mv, None -> mv.MaximumMovementPoints
    | _ , _ -> 0




