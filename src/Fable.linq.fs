module Fable.Linq.Main
open System.Linq

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
   member x.SumBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'U ) = 
      match source with 
         | [] ->  LanguagePrimitives.GenericZero< 'U >
         | t ->
             let mutable acc = LanguagePrimitives.GenericZero< 'U >
             for x in t do
                 acc <- Checked.(+) acc (f x)
             acc

   [<CustomOperation("groupBy", MaintainsVariableSpace=true)>]
   member x.GroupBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key)  = 
      List.groupBy f source 
      
   [<CustomOperation("all", MaintainsVariableSpace=true)>]
   member x.All ( source:List<'T>, [<ProjectionParameter>] f:'T -> bool)  = 
      List.exists (fun a -> a |> (f >> not)) source  |> not
     
   [<CustomOperation("averageBy", MaintainsVariableSpace=true)>]
   member x.AverageBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'U) = 
      match source with 
         | [] -> invalidArg "source" LanguagePrimitives.ErrorStrings.InputSequenceEmptyString
         | xs ->
             let mutable sum = LanguagePrimitives.GenericZero< 'U >
             let mutable count = 0
             for x in xs do
                 sum <- Checked.(+) sum (f x)
                 count <- count + 1
             sum 


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
       
   [<CustomOperation("exists", MaintainsVariableSpace=true)>]
   member x.Exists ( source:List<'T>, [<ProjectionParameter>] f:'T -> bool)  = 
      List.exists f source
       
   [<CustomOperation("find", MaintainsVariableSpace=true)>]
   member x.Find ( source:List<'T>, [<ProjectionParameter>] f:'T -> bool)  = 
      List.find f source

   // member this.GroupJoin : QuerySource<'Outer,'Q> * QuerySource<'Inner,'Q> * ('Outer -> 'Key) * ('Inner -> 'Key) * ('Outer -> seq<'Inner> -> 'Result) -> QuerySource<'Result,'Q> = jsNative
   // member this.GroupValBy : List<'T> * ('T -> 'Value) * ('T -> 'Key) -> QuerySource<IGrouping<'Key,'Value>,'Q> = jsNative
   // member this.HeadOrDefault : List<'T> -> 'T = jsNative
   // member this.Join : QuerySource<'Outer,'Q> * QuerySource<'Inner,'Q> * ('Outer -> 'Key) * ('Inner -> 'Key) * ('Outer -> 'Inner -> 'Result) -> QuerySource<'Result,'Q> = jsNative
   // member this.LastOrDefault : List<'T> -> 'T = jsNative
   // member this.LeftOuterJoin : QuerySource<'Outer,'Q> * QuerySource<'Inner,'Q> * ('Outer -> 'Key) * ('Inner -> 'Key) * ('Outer -> seq<'Inner> -> 'Result) -> QuerySource<'Result,'Q> = jsNative
   // member this.MaxByNullable : List<'T> * ('T -> Nullable<'Value>) -> Nullable<'Value> = jsNative
   // member this.MinByNullable : List<'T> * ('T -> Nullable<'Value>) -> Nullable<'Value> = jsNative
   // member this.Quote : Expr<'T> -> Expr<'T> = jsNative
   // member this.Run : Expr<QuerySource<'T,IQueryable>> -> IQueryable<'T> = jsNative
   // member this.SortByNullable : List<'T> * ('T -> Nullable<'Key>) -> List<'T> = jsNative
   // member this.SortByNullableDescending : List<'T> * ('T -> Nullable<'Key>) -> List<'T> = jsNative
   // member this.Source : IEnumerable<'T> -> QuerySource<'T,IEnumerable> = jsNative
   // member this.Source : IQueryable<'T> -> List<'T> = jsNative
   // member this.SumBy : List<'T> * ('T -> ^Value) -> ^Value = jsNative
   // member this.SumByNullable : List<'T> * ('T -> Nullable<^Value>) -> Nullable<^Value> = jsNative
   // member this.ThenByNullable : List<'T> * ('T -> Nullable<'Key>) -> List<'T> = jsNative
   // member this.ThenByNullableDescending : List<'T> * ('T -> Nullable<'Key>) -> List<'T> = jsNative
   // member this.YieldFrom : List<'T> -> List<'T> = jsNative
   [<CustomOperation("join", IsLikeJoin=true)>]
   member x.Join (outer : List<'T>, inner: List<'T>, f1: 'T -> 'Key, f2: 'T -> 'Key, f3: ('T * 'T) -> 'Key)  = 
      let mutable ret = []
      for x in outer do
         let m = inner |> List.find (fun y -> 
            f1 x = f2 y
         ) 
         if Some(m).IsNone |> not then
            ret <-  List.append ret (f3 (m ,x))
      ret
      
   [<CustomOperation("minBy", MaintainsVariableSpace=true)>]
   member x.MinBy (source:List<'T>, [<ProjectionParameter>] f:'T -> 'U)  = 
      List.minBy f source

   [<CustomOperation("maxBy", MaintainsVariableSpace=true)>]
   member x.MaxBy (source:List<'T>, [<ProjectionParameter>] f:'T -> 'U)  = 
      List.maxBy f source
   
   [<CustomOperation("nth", MaintainsVariableSpace=true)>]
   member x.Nth (source:List<'T>, [<ProjectionParameter>] i: int)  = 
      List.item i source 

   [<CustomOperation("head", MaintainsVariableSpace=true)>]
   member x.Head ()  = 
      List.head

   [<CustomOperation("last", MaintainsVariableSpace=true)>]
   member x.Last ()  = 
      List.last

   [<CustomOperation("skip", MaintainsVariableSpace=true)>]
   member x.Skip (v:int)  = 
      List.skip v
   
   [<CustomOperation("skipWhile", MaintainsVariableSpace=true)>]
   member x.SkipWhile (source:List<'T>, f:'T -> bool)  = 
      List.skipWhile f source

   [<CustomOperation("take", MaintainsVariableSpace=true)>]
   member x.Take (v:int)  = 
      List.take v

   [<CustomOperation("takeWhile", MaintainsVariableSpace=true)>]
   member x.TakeWhile (source:List<'T>, f:'T -> bool)  = 
      List.takeWhile f source

   [<CustomOperation("zero", MaintainsVariableSpace=true)>]
   member x.Zero ()  = 
      List.empty

let fablequery = FableQueryBuilder()
let b = [2]

let m = query {
   for a in [1] do
   join j in b on (a = j) 
   select a
} 
