
namespace Support

open System

type GroundClass = | Normal | Frozen | Muddy
type TerrainClass =  | Ground of GroundClass | Sea | Air
type WeatherCondition = | Clear | Cloudy | Raining | Snowing

type InfantryClass = | Regular | Engineer | Bridging | Ranger | Airborne
type LandClass = | Infantry of InfantryClass| Tank | TankDestroyer | AntiTank | AirDefense | LandTransport
type NavalClass = | CapitalShip | AircraftCarrier | Submarine | SeaTransport
type AirClass = | Fighter | TactialBomber | StrategicBomber | AirTransport
type UnitClass = | Land of LandClass | Naval of NavalClass | Air of AirClass
type AlliedNation = | US | UK | NZ | AU | PO | FR | USSR 
type AxisNation = | GR | IT 
type Faction = | Allied of AlliedNation | Axis of AxisNation


type Unit = {Id:int; UnitClass: UnitClass; }

//Attacker and Defender -> primary combatants
//Most battles have 2 vollies:
//Attacker is the Aggressor, Defender is the Protector
//Defender is the Aggressor, Attacker is the Protector
//Who goes 1st is determined by initative
//Some battles have 1 volley
//Support Unit Is the Agressor, Attacker is the Protector

//Volley is a calculation of how much damage each unit receives
//Damage is detertmined by Attack Value, Defense Value, modified by terrane and weather
//Once the Volley is exchanged, determine if the units are detroyed, 
//and if surive, does the defeneder retreat?

//Map
//X is across, Y is Down
//Each tile has a number
//Each tile has some characteristics
//Empty
//Units (can be stacked)
//Earth
    //Land
    //LandTransport
    //Air
    //Air -> Land
    //Air -> LandTransport
    //AirTransport -> Land
    //AirTransport -> LandTransport
//Sea
    //Naval
    //NavalTransport
    //Air -> Naval
    //Air -> NavalTransport


//Types of battles
//Land -> Land
//Land -> LandTransport
//Land -> Air
//Land -> Naval
//LandTransport -> Land
//Naval -> Naval
//Naval -> NavalTransport
//Naval -> Land



//Move
//Attack

//type AttackerBattleOutcome =
//    | Destroyed
//    | Survives

//type DefenderBattleOutcome =
//    | Destroyed
//    | Holds
//    | Retreats

//type Tile = {Id: int}
//type Unit = {Id: int; Strength: int}

//type Combatant = {Tile: Tile; Unit: Unit}

//type BattleInput = {Id:int}

//type Initiative =
    //| Attacker
    //| Defender
    //| Simultanous

//let calculateBattle (input:BattleInput) =
    ////Surprised
    //let hasSupportingUnit = false
    //match hasSupportingUnit with
    //| true -> calculateVolleyBattle
    //| false -> calculateNormalBattle
    ////Supporting Unit