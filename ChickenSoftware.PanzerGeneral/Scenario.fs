
module ChickenSoftware.PanzerGeneral.Scenario

open Tile
open System
open Nation

type Player =
| Human of Nation
| Computer of Nation

type Scenario = {CurrentPlayer: Player; Board: Tile array}

