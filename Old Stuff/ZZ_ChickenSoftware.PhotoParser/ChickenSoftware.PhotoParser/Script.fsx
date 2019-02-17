
//Clapton Junction

open System
open System.IO
open System.Drawing

let width = 60
let height = 50
let path = "//Users//jamesdixon//GitHub//ChickenSoftware.PhotoParser//ChickenSoftware.PhotoParser//images//tacmap_dry.jpg"
let sourceImage = Image.FromFile(path) //width = 1200, height = 600, so n = 240
let bitmap = new Bitmap(width,height)
let graphics = Graphics.FromImage(bitmap)
let targetRectangle = new Rectangle(0,0,width,height)
let sourceRectangle = new Rectangle(0,0,width,height)
graphics.DrawImage(sourceImage,targetRectangle,sourceRectangle,GraphicsUnit.Pixel)
graphics.Flush()
let fileName = width.ToString() + "_" + height.ToString() + ".jpeg"
bitmap.Save(fileName)
//Fails b/c bitmap is open by the REPL -> Sigh!
graphics.Dispose()

let createHexImage x y =
    let width = 60
    let height = 50
    let path = "//Users//jamesdixon//GitHub//ChickenSoftware.PhotoParser//ChickenSoftware.PhotoParser//images//tacmap_dry.jpg"
    let sourceImage = Image.FromFile(path) //width = 1200, height = 600, so n = 240
    let bitmap = new Bitmap(width,height)
    let graphics = Graphics.FromImage(bitmap)
    let targetRectangle = new Rectangle(0,0,width,height)
    let sourceRectangle = new Rectangle(0,0,width*x,height*y)
    graphics.DrawImage(sourceImage,targetRectangle,sourceRectangle,GraphicsUnit.Pixel)
    graphics.Flush()
    let fileName = x.ToString() + "_" + y.ToString() + ".jpeg"
    //bitmap.Save(fileName)
    //Fails b/c bitmap is open by the REPL -> Sigh!
    graphics.Dispose()

createHexImage 1 1

//12 rows by 20 columns
[0..12]
|> Seq.iter(fun x -> [0..20] |> Seq.iter(fun y -> (printfn "x=%i y=%i" x y)))




