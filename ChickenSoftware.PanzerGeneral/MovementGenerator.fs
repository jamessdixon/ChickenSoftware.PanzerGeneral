
module ChickenSoftware.PanzerGeneral.MovementGenerator

open System.IO
open FSharp.Data
open System.Reflection


let getJson (assembly:Assembly) (fileName:string) =
    let stream = assembly.GetManifestResourceStream(fileName);
    let reader = new StreamReader(stream)
    reader.ReadToEnd()


