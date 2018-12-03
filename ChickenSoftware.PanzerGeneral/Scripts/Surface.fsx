
#r "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/packages/Xamarin.Forms.3.0.0.482510/lib/netstandard2.0/Xamarin.Forms.Core.dll"
#r "/Users/jamesdixon/GitHub/chickensoftware.xf/Droid/obj/Debug/linksrc/netstandard.dll"

#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Unit.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Nation.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Terrain.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Tile.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Board.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Hex.fsx"

open Tile
open Board
open Terrain
open Xamarin.Forms

let populateSurface (scenarioId:int) () =
    let board = createBoard scenarioId LandCondition.Dry
    let layout = new AbsoluteLayout()
    let hexes = 
        board
        |> Array.map(fun t -> createHex t 1.0)
        |> Array.iter(fun h -> layout.Children.Add(h))

    let scrollView = new ScrollView()
    scrollView.Orientation <- ScrollOrientation.Both
    scrollView.Content <- layout
    scrollView
