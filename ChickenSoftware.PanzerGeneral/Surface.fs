
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

let populateSurface (assembly:Assembly) (scenarioId:int) =
    let layout = new AbsoluteLayout()
    let board = createBoard assembly scenarioId LandCondition.Dry
    let scale = 1.5
    board 
    |> Array.map(fun t -> createHex t scale)
    |> Array.iter(fun h -> layout.Children.Add(h))

    let scrollView = new ScrollView()
    scrollView.Orientation <- ScrollOrientation.Both
    scrollView.Content <- layout
    scrollView
