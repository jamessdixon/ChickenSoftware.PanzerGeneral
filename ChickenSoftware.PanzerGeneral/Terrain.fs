
module ChickenSoftware.PanzerGeneral.Terrain

type LandCondition = 
| Dry 
| Frozen 
| Muddy 

type BaseTerrain = {Id: int; Condition: LandCondition}

type Road = Road
type River = River

type Improvements = Road Option * River Option

type Land =
| Clear of BaseTerrain * Improvements
| Fortificaiton of BaseTerrain * Improvements
| Bocage of BaseTerrain * Improvements
| Rough of BaseTerrain * Improvements
| City of BaseTerrain
| Airfield of BaseTerrain 
| Mountain of BaseTerrain
| Forest of BaseTerrain
| Swamp of BaseTerrain
| Desert of BaseTerrain
| RoughDesert of BaseTerrain
| Escarpment of BaseTerrain

type Terrain =
| Land of Land
| Sea of BaseTerrain
| Port of BaseTerrain 

let getImprovments (roadInd: int) (riverInd: int) =
    match roadInd, riverInd with
    | 1, 1 -> Some Road, Some River
    | 1, 0 -> Some Road, None
    | 0, 1 -> None, Some River
    | _, _ -> None, None

let getLand (baseTerrain:BaseTerrain) roadInd riverInd =
    let improvements = getImprovments roadInd riverInd
    match baseTerrain.Id with 
    | 2 -> Land.Rough (baseTerrain, improvements)
    | 3 -> Land.Mountain baseTerrain
    | 4 -> Land.City baseTerrain
    | 5 -> Land.Clear (baseTerrain,improvements)
    | 6 -> Land.Forest baseTerrain
    | 7 -> Land.Swamp baseTerrain
    | 8 -> Land.Airfield baseTerrain
    | 9 -> Land.Fortificaiton (baseTerrain,improvements)
    | 10 -> Land.Bocage (baseTerrain,improvements)
    | 11 -> Land.Desert baseTerrain
    | 12 -> Land.RoughDesert baseTerrain
    | 13 -> Land.Escarpment baseTerrain
    | _ -> Land.Clear (baseTerrain, improvements)

let getBaseTerrain (terrainId: int) (landCondition:LandCondition) (roadInd: int) (riverInd: int)  =
    let baseTerrain = {Id = terrainId; Condition = landCondition}
    match terrainId with 
    | 0 -> Terrain.Sea baseTerrain
    | 1 -> Terrain.Port baseTerrain
    | _ -> Terrain.Land (getLand baseTerrain roadInd riverInd)
