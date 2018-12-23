module Fable.Linq.Main

type QueryBuilder() = 
    member x.For(tz:List<'T>, f:'T -> 'T) : List<'T> = 
        List.map (f) tz
    member x.Yield(v:'T) : 'T = 
        v

    [<CustomOperation("where", MaintainsVariableSpace=true)>]
    member x.Where ( source:List<'T>, [<ProjectionParameter>] f:'T -> bool ) : List<'T> = 
        List.filter (f) source

    [<CustomOperation("select", MaintainsVariableSpace=true)>]
    member x.Select ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'T2 ) : List<'T2> = 
       List.map (fun a -> f(a)) source 
let fablequery = QueryBuilder()


