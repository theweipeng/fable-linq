module Fable.Linq.Main

type FableQueryBuilder() = 
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

    [<CustomOperation("sortBy", MaintainsVariableSpace=true)>]
    member x.SortBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key) : List<'T> = 
       List.sortBy (fun a -> f(a)) source 
    
    [<CustomOperation("thenBy", MaintainsVariableSpace=true)>]
    member x.ThenBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key ) : List<'T> = 
       List.sortBy (fun a -> f(a)) source 

    [<CustomOperation("sortByDescending", MaintainsVariableSpace=true)>]
    member x.SortByDescending ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key) : List<'T> = 
       List.sortByDescending (fun a -> f(a)) source 
    
    [<CustomOperation("thenByDescending", MaintainsVariableSpace=true)>]
    member x.ThenByDescending ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key) : List<'T> = 
       List.sortByDescending (fun a -> f(a)) source 
//'sortBy', 'thenBy', 'groupBy', 'groupValBy', 
//'join', 'groupJoin', 'sumBy' and 'averageBy
let fablequery = FableQueryBuilder()


