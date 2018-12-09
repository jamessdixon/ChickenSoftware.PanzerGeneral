module ChickenSoftware.PanzerGeneral.Movement

open Unit
open UnitMapper

type UnitMovementType =
    | Tracked
    | HalfTracked
    | Wheeled
    | Walk
    | None
    | Air
    | Water
    | AllTerrain

let getUnitMovementTypeId (unitMovementType:UnitMovementType) =
    match unitMovementType with
    | Tracked -> 1
    | HalfTracked -> 2
    | Wheeled -> 3
    | Walk -> 4
    | None -> 5
    | Air -> 6
    | Water -> 7
    | AllTerrain -> 8

let getUnitMovementType (unit: Unit) =
    match unit with 
    | Unit.Combat c -> 
        match c with 
        | Combat.Air ac -> UnitMovementType.Air
        | Combat.Land lc ->
            match lc with 
            | LandCombat.AirDefense lcad ->
                match lcad with 
                | AirDefense.SelfPropelled lcadsp -> UnitMovementType.Tracked
                | AirDefense.Towed lcadt -> UnitMovementType.Walk
            | LandCombat.AntiAir lcaa -> UnitMovementType.Tracked
            | LandCombat.AntiTank lcat -> UnitMovementType.Walk
            | LandCombat.Artillery lca ->
                match lca with
                | Artillery.Towed lcat -> UnitMovementType.Walk
                | Artillery.SelfPropelled lcasp -> UnitMovementType.Tracked  
            | LandCombat.Emplacement lce -> UnitMovementType.None
            | LandCombat.Infantry lci -> UnitMovementType.Walk
            | LandCombat.Recon lcr -> UnitMovementType.Tracked
            | LandCombat.Tank lct -> UnitMovementType.Tracked
            | LandCombat.TankDestroyer lctd -> UnitMovementType.Tracked
        | Combat.Naval nc -> UnitMovementType.Water
    | Unit.Transport t -> 
        match t with 
        | Transport.Air at -> UnitMovementType.Air
        | Transport.Land lt -> UnitMovementType.Wheeled
        | Transport.Naval nt -> UnitMovementType.Water

