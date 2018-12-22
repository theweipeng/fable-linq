module Fable.Linq.Main
open Fable.Core
open Fable.Import.JS
open Fable.Core
open Fable.Import

type SimpleQueryBuilder() = 
    member x.For(tz:List<'T>, f:'T -> 'T) : List<'T> = 
        List.map (f) tz
    member x.Yield(v:'T) : 'T = 
        v

    [<CustomOperation("where", MaintainsVariableSpace=true)>]
    member x.Where ( source:List<'T>, [<ProjectionParameter>] f:'T -> bool ) : List<'T> = 
        source |> Browser.console.log
        let ret = List.filter (f) source
        ret.Length |> Browser.console.log
        ret
let fablequery = SimpleQueryBuilder()


