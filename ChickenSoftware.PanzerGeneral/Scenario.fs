
module ChickenSoftware.PanzerGeneral.Scenario

open Tile
open Board
open System
open Nation
open Terrain
open System.Reflection

open Tile
open Board
open Nation
open Terrain
open UnitMapper
open SupportData
open MovementCalculator

type Player =
| Human of Nation
| Computer of Nation

type BoardMode =
| Earth
| Sky

type Scenario = {
                PlayerOne: Player;
                PlayerTwo: Player;
                CurrentPlayer: Player; 
                CurrentLandCondition: LandCondition;
                Board: Tile array; 
                BoardMode: BoardMode;
                ActiveTile: Tile option; 
                MovableTiles: Tile array option;
                AttackableTiles: Tile array option}


let initializeScenario assembly =
    let board = createBoard assembly 0 LandCondition.Dry 
    let humanPlayer = Player.Human (Nation.Axis AxisNation.German)
    let computerPlayer = Player.Computer (Nation.Allied AlliedNation.Poland)

    {PlayerOne = humanPlayer;
    PlayerTwo = computerPlayer;
    CurrentPlayer=humanPlayer; 
    CurrentLandCondition = LandCondition.Dry;
    Board = board; 
    BoardMode = BoardMode.Earth;
    ActiveTile = None; 
    MovableTiles = None; 
    AttackableTiles=None}

let updateScenarioForPlayer (scenario: Scenario) =
    match scenario.CurrentPlayer with 
    | Player.Human h -> {scenario with CurrentPlayer=scenario.PlayerTwo}
    | Player.Computer c -> {scenario with CurrentPlayer=scenario.PlayerOne}

let deactivateUnit (scenario: Scenario) (tile:Tile) =
    let baseTile = getBaseTile tile
    match scenario.BoardMode with 
    | BoardMode.Earth ->
        match baseTile.EarthUnit with 
        | Some eu -> {scenario with ActiveTile=None}
        | None -> scenario
    | BoardMode.Sky ->
        match baseTile.SkyUnit with 
        | Some su -> {scenario with ActiveTile=None}
        | None -> scenario

let activateUnit (scenario: Scenario) (tile:Tile) =
    let baseTile = getBaseTile tile
    match scenario.BoardMode with 
    | BoardMode.Earth ->
        match baseTile.EarthUnit with 
        | Some eu -> {scenario with ActiveTile=Some tile}
        | None -> scenario
    | BoardMode.Sky ->
        match baseTile.SkyUnit with 
        | Some su -> {scenario with ActiveTile=Some tile}
        | None -> scenario

let moveUnit (scenario: Scenario)(targetTile:Tile) =
    let board = scenario.Board
    let sourceTile = scenario.ActiveTile.Value
    let sourceBaseTile = getBaseTile sourceTile
    let targetBaseTile = getBaseTile targetTile
    let updatedTargetBaseTile = {targetBaseTile with EarthUnit = sourceBaseTile.EarthUnit}
    let updatedSourceBaseTile = {sourceBaseTile with EarthUnit = None}
    let updatedTargetTile = 
        match targetTile with 
        | Regular bt -> Tile.Regular updatedTargetBaseTile
        | Victory vt -> Tile.Victory {vt with VictoryTile.BaseTile = updatedTargetBaseTile}
    let updatedSourceTile = 
        match sourceTile with 
        | Regular bt -> Tile.Regular updatedSourceBaseTile
        | Victory vt -> Tile.Victory {vt with VictoryTile.BaseTile = updatedSourceBaseTile}
    let targetIndex = board |> Array.findIndex(fun t -> t = targetTile)
    let sourceIndex = board |> Array.findIndex(fun t -> t = sourceTile)
    board.[targetIndex] <- updatedTargetTile
    board.[sourceIndex] <- updatedSourceTile
    scenario
                


        