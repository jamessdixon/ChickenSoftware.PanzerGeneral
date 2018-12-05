module ChickenSoftware.PanzerGeneral.Movement

open Unit
open UnitMapper

type MovementType =
    | Tracked
    | HalfTracked
    | Wheeled
    | Walk
    | None
    | Air
    | Water
    | AllTerrain

//let getMovementType (unit: Unit) =
    
