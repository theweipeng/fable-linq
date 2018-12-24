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
      List.map f source 

   [<CustomOperation("sortBy", MaintainsVariableSpace=true)>]
   member x.SortBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key) : List<'T> = 
      List.sortBy f source 
    
   [<CustomOperation("thenBy", MaintainsVariableSpace=true)>]
   member x.ThenBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key ) : List<'T> = 
      List.sortBy f source 

   [<CustomOperation("sortByDescending", MaintainsVariableSpace=true)>]
   member x.SortByDescending ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key) : List<'T> = 
      List.sortByDescending f source 
    
   [<CustomOperation("thenByDescending", MaintainsVariableSpace=true)>]
   member x.ThenByDescending ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key) : List<'T> = 
      List.sortByDescending f source 
   
   [<CustomOperation("sumBy", MaintainsVariableSpace=true)>]
   member x.SumBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'U) : 'U when 
               'U :  (static member (+) : ^T * ^T -> ^T) 
               and 'U : (static member Zero : unit-> List<'T>)  = 
      null //todo

   [<CustomOperation("groupBy", MaintainsVariableSpace=true)>]
   member x.GroupBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key)  = 
      List.groupBy f source 
    
   [<CustomOperation("all", MaintainsVariableSpace=true)>]
   member x.All ( source:List<'T>, [<ProjectionParameter>] f:'T -> bool)  = 
      List.exists (fun a -> a |> (f >> not)) source  |> not
     
   [<CustomOperation("averageBy", MaintainsVariableSpace=true)>]
   member x.AverageBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'U): 'U  when 
               'U :  (static member (+) : ^T * ^T -> ^T) 
               and 'U : (static member DivideByInt : ^T * ^T -> ^T) 
               and 'U : (static member Zero : unit-> List<'T>)  = 
      null


   [<CustomOperation("contains", MaintainsVariableSpace=true)>]
   member x.Contains ( source:List<'T>, [<ProjectionParameter>] v:'T )  = 
      List.contains v source
    
   [<CustomOperation("count", MaintainsVariableSpace=true)>]
   member x.Count ( source:List<'T>)  = 
      source.Length
   
   [<CustomOperation("distinct", MaintainsVariableSpace=true)>]
   member x.Distinct ( source:List<'T>)  = 
      List.distinct source
   
   [<CustomOperation("exactlyOne", MaintainsVariableSpace=true)>]
   member x.ExactlyOne ( source:List<'T>)  = 
      List.exactlyOne source
   
   [<CustomOperation("exactlyOneOrDefault", MaintainsVariableSpace=true)>]
   member x.ExactlyOneOrDefault ( source:List<'T>)  = 
      List.exactlyOne source  //todo
       
//'sortBy', 'thenBy', 'groupBy', 'groupValBy', 
//'join', 'groupJoin', 'sumBy' and 'averageBy
let fablequery = FableQueryBuilder()

let m = query {
   for a in [1] do
   
   count 
}
