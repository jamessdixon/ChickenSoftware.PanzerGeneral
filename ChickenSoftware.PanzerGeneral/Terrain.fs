
module ChickenSoftware.PanzerGeneral.Terrain

type LandCondition = 
| Dry 
| Frozen 
| Muddy 

type BaseTerrain = {Id: int; Condition: LandCondition}

type Road = Road
type River = River

type Improvements = {Road: Road Option; River: River Option}
type ImprovedTerrain = {BaseTerrain: BaseTerrain; Improvements: Improvements}

type Land =
| Clear of ImprovedTerrain
| Fortificaiton of ImprovedTerrain
| Bocage of ImprovedTerrain
| Rough of ImprovedTerrain
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
    | 1, 1 -> {Road = Some Road; River = Some River}
    | 1, 0 -> {Road = Some Road; River = None}
    | 0, 1 -> {Road = None; River = Some River}
    | _, _ -> {Road = None; River = None}

let getLand (baseTerrain:BaseTerrain) roadInd riverInd =
    let improvements = getImprovments roadInd riverInd
    match baseTerrain.Id with 
    | 2 -> Land.Rough {BaseTerrain = baseTerrain; Improvements = improvements}
    | 3 -> Land.Mountain baseTerrain
    | 4 -> Land.City baseTerrain
    | 5 -> Land.Clear {BaseTerrain = baseTerrain; Improvements = improvements}
    | 6 -> Land.Forest baseTerrain
    | 7 -> Land.Swamp baseTerrain
    | 8 -> Land.Airfield baseTerrain
    | 9 -> Land.Fortificaiton {BaseTerrain = baseTerrain; Improvements = improvements}
    | 10 -> Land.Bocage {BaseTerrain = baseTerrain; Improvements = improvements}
    | 11 -> Land.Desert baseTerrain
    | 12 -> Land.RoughDesert baseTerrain
    | 13 -> Land.Escarpment baseTerrain
    | _ -> Land.Clear {BaseTerrain = baseTerrain; Improvements = improvements}

let getBaseTerrain (terrainId: int) (landCondition:LandCondition) (roadInd: int) (riverInd: int)  =
    let baseTerrain = {Id = terrainId; Condition = landCondition}
    match terrainId with 
    | 0 -> Terrain.Sea baseTerrain
    | 1 -> Terrain.Port baseTerrain
    | _ -> Terrain.Land (getLand baseTerrain roadInd riverInd)

let getBaseTerrainFromTerrain terrain =
    match terrain with 
    | Terrain.Sea bt -> bt
    | Terrain.Port bt -> bt
    | Terrain.Land l -> 
        match l with
        | Land.Airfield bt -> bt
        | Land.Bocage it -> it.BaseTerrain
        | Land.City bt -> bt
        | Land.Clear it -> it.BaseTerrain
        | Land.Desert bt -> bt
        | Land.Escarpment bt -> bt
        | Land.Forest bt -> bt
        | Land.Fortificaiton it -> it.BaseTerrain
        | Land.Mountain bt -> bt
        | Land.Rough it -> it.BaseTerrain
        | Land.RoughDesert bt -> bt
        | Land.Swamp bt -> bt

let getLandConditionId landCondition =
    match landCondition with 
    | LandCondition.Dry -> 0
    | LandCondition.Muddy -> 1
    | LandCondition.Frozen -> 2

let getLandTypeId (lnd:Land) = 
    match lnd with
    | Land.Rough x -> 2
    | Land.Mountain x -> 3
    | Land.City x -> 4
    | Land.Clear x -> 5
    | Land.Forest x -> 6
    | Land.Swamp x -> 7
    | Land.Airfield x -> 8
    | Land.Fortificaiton x -> 9
    | Land.Bocage x -> 10
    | Land.Desert x -> 11
    | Land.RoughDesert x -> 12
    | Land.Escarpment x -> 13

let getTerrainTypeId (terrain: Terrain) =
    match terrain with
    | Terrain.Land l -> getLandTypeId l
    | Terrain.Sea bt -> 0
    | Terrain.Port bt -> 1

