
module ChickenSoftware.PanzerGeneral.Surface

open Hex
open Tile
open Board
open Terrain
open Xamarin.Forms
open System.Reflection

let createLayout numberOfTiles scale =
    let width = 60.0 * scale
    let height = 50.0 * scale
    let layout = new AbsoluteLayout()
    layout.WidthRequest <- 12.0 * width
    let rows = numberOfTiles / 20 |> float
    layout.HeightRequest <- rows * height
    layout

let createSurface (board:Tile array) =
    let scale = 1.5
    let numberOfTiles = board |> Seq.length
    let layout = createLayout numberOfTiles scale
    board 
    |> Array.iter(fun t -> createHex layout t scale)
    let scrollView = new ScrollView()
    scrollView.Orientation <- ScrollOrientation.Both
    scrollView.Content <- layout
    scrollView
    


        