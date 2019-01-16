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
   member x.SumBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'U ) = 
      match source with 
         | [] ->  LanguagePrimitives.GenericZero< 'U >
         | t ->
             let mutable acc = LanguagePrimitives.GenericZero< 'U >
             for x in t do
                 acc <- Checked.(+) acc (f x)
             acc

   [<CustomOperation("groupBy", AllowIntoPattern=true, MaintainsVariableSpace=true)>]
   member x.GroupBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> 'Key)  = 
      List.groupBy f source 
      
   [<CustomOperation("all", MaintainsVariableSpace=true)>]
   member x.All ( source:List<'T>, [<ProjectionParameter>] f:'T -> bool)  = 
      List.exists (fun a -> a |> (f >> not)) source  |> not
     
   [<CustomOperation("averageBy", MaintainsVariableSpace=true)>]
   member x.AverageBy ( source:List<'T>, [<ProjectionParameter>] f:'T -> int) = 
      match source with 
         | [] -> invalidArg "source" "cant be empty"
         | xs ->
             let mutable sum = 0
             let mutable count = 0
             for x in xs do
                 sum <- Checked.(+) sum (f x)
                 count <- count + 1
             sum / count


   [<CustomOperation("contains", MaintainsVariableSpace=true)>]
   member x.Contains ( source:List<'T>, [<ProjectionParameter>] v:'T -> 'T )  = 
      List.exists (fun x -> x = v x) source
    
   [<CustomOperation("count", MaintainsVariableSpace=true)>]
   member x.Count ( source:List<'T>)  = 
      source.Length
   
   [<CustomOperation("distinct", MaintainsVariableSpace=true)>]
   member x.Distinct ( source:List<'T>)  = 
      List.distinct source
   
   [<CustomOperation("exactlyOne", MaintainsVariableSpace=true)>]
   member x.ExactlyOne ( source:List<'T>)  = 
      List.exactlyOne source
       
   [<CustomOperation("exists", MaintainsVariableSpace=true)>]
   member x.Exists ( source:List<'T>, [<ProjectionParameter>] f:'T -> bool)  = 
      List.exists f source
       
   [<CustomOperation("find", MaintainsVariableSpace=true)>]
   member x.Find ( source:List<'T>, [<ProjectionParameter>] f:'T -> bool)  = 
      List.find f source

   // member this.HeadOrDefault : List<'T> -> 'T = jsNative
   // member this.LastOrDefault : List<'T> -> 'T = jsNative
   // member this.MaxByNullable : List<'T> * ('T -> Nullable<'Value>) -> Nullable<'Value> = jsNative
   // member this.MinByNullable : List<'T> * ('T -> Nullable<'Value>) -> Nullable<'Value> = jsNative
   // member this.Quote : Expr<'T> -> Expr<'T> = jsNative
   // member this.Run : Expr<QuerySource<'T,IQueryable>> -> IQueryable<'T> = jsNative
   // member this.SortByNullable : List<'T> * ('T -> Nullable<'Key>) -> List<'T> = jsNative
   // member this.SortByNullableDescending : List<'T> * ('T -> Nullable<'Key>) -> List<'T> = jsNative
   // member this.SumByNullable : List<'T> * ('T -> Nullable<^Value>) -> Nullable<^Value> = jsNative
   // member this.ThenByNullable : List<'T> * ('T -> Nullable<'Key>) -> List<'T> = jsNative
   // member this.ThenByNullableDescending : List<'T> * ('T -> Nullable<'Key>) -> List<'T> = jsNative
   // member this.YieldFrom : List<'T> -> List<'T> = jsNative
   [<CustomOperation("join", IsLikeJoin=true, MaintainsVariableSpace=true)>]
   member x.Join (outer : List<'T>, inner: List<'T>, [<ProjectionParameter>]outerKeySelector, [<ProjectionParameter>]innerKeySelector, [<ProjectionParameter>]resultSelector: 'T -> 'T -> 'U)  = 
      let mutable ret = []
      for x in outer do
         let m = inner |> List.filter (fun y -> 
            outerKeySelector x = innerKeySelector y
         )
         for y in m do
            ret <-  (resultSelector x y) :: ret
      ret
   
   [<CustomOperation("leftOuterJoin", IsLikeGroupJoin=true)>]
   member x.LeftOuterJoin (outer : List<'T>, inner: List<'T>, [<ProjectionParameter>]outerKeySelector, [<ProjectionParameter>]innerKeySelector, [<ProjectionParameter>]resultSelector:  'T -> List<'T> -> 'U)  = 
      let mutable ret = []
      for x in outer do
         let m = inner |> List.filter (fun y -> 
            outerKeySelector x = innerKeySelector y
         ) 
         ret <-  (resultSelector x m) :: ret
      ret
   


   [<CustomOperation("groupJoin", IsLikeGroupJoin=true, MaintainsVariableSpace=true)>]
   member x.GroupJoin (outer : List<'T>, inner: List<'T>, outerKeySelector, innerKeySelector, resultSelector: 'T -> List<'T> -> 'U)  = 
      let mutable ret = []
      for x in outer do
         let m = inner |> List.filter (fun y -> 
            outerKeySelector x = innerKeySelector y
         ) 
         ret <-  (resultSelector x m) :: ret
      ret

   [<CustomOperation("groupValBy", AllowIntoPattern=true, MaintainsVariableSpace=true)>]
   member x.GroupValBy<'T,'Key,'Result when 'Key : equality > (source:List<'T>, [<ProjectionParameter>]resultSelector: 'T -> 'Result, [<ProjectionParameter>]keySelector: 'T -> 'Key)    = 
      let groupRet = source |> List.groupBy keySelector
      let mutable ret = []
      for x in groupRet do
         match x with
            | (a, b) -> ret <- (a, List.map resultSelector b) :: ret
      ret

   [<CustomOperation("minBy")>]
   member x.MinBy (source:List<'T>, [<ProjectionParameter>] f:'T -> 'U)  = 
      List.minBy f source

   [<CustomOperation("maxBy")>]
   member x.MaxBy (source:List<'T>, [<ProjectionParameter>] f:'T -> 'U)  = 
      List.maxBy f source

   [<CustomOperation("head")>]
   member x.Head (source:List<'T>)  = 
      List.head source

   [<CustomOperation("last")>]
   member x.Last (source:List<'T>)  = 
      List.last source

   [<CustomOperation("skip")>]
   member x.Skip (source:List<'T>, v:int)  = 
      List.skip v source
   
   [<CustomOperation("skipWhile")>]
   member x.SkipWhile (source:List<'T>, [<ProjectionParameter>] f:'T -> bool)  = 
      List.skipWhile f source

   [<CustomOperation("take")>]
   member x.Take (source:List<'T>, v:int)  = 
      List.take v source

   [<CustomOperation("takeWhile")>]
   member x.TakeWhile (source:List<'T>, [<ProjectionParameter>] f:'T -> bool)  = 
      List.takeWhile f source

   [<CustomOperation("nth")>]
   member x.Nth (source:List<'T>, i: int)  = 
      List.item i source
let fablequery = FableQueryBuilder()
