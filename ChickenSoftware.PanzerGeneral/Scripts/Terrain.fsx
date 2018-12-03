
type LandCondition = 
| Dry 
| Frozen 
| Muddy 

type BaseTerrain = {Id: int; Condition: LandCondition}

//They put that 1 city/road just to mess with me Col 7. Row 6
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

let terrain = Land (Land.City {Id=0; Condition = LandCondition.Dry})
let improvements = Some Road, None
let land1 = Land.Clear ({Id=1; Condition = LandCondition.Dry}, improvements)






