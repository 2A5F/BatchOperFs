module BatchOper

type BatchOrBuilder() =
    member __.Yield _ = fun c x -> c x

    member __.Run f = fun x -> (f <| fun _ -> false) x

    [<CustomOperation("eq", AllowIntoPattern = true)>]
    /// Equal
    member __.Equals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() = x || c x

    [<CustomOperation("ne", AllowIntoPattern = true)>]
    /// Not equal
    member __.NotEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <> x || c x

    [<CustomOperation("gt", AllowIntoPattern = true)>]
    /// Greater than
    member __.GreaterThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() > x || c x

    [<CustomOperation("lt", AllowIntoPattern = true)>]
    /// Less than
    member __.LessThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() < x || c x

    [<CustomOperation("ge", AllowIntoPattern = true)>]
    /// Greater then or equal
    member __.GreaterEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() >= x || c x

    [<CustomOperation("le", AllowIntoPattern = true)>]
    /// Less then or equal
    member __.LessEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <= x || c x

type BatchAndBuilder() =
    member __.Yield _ = fun c x -> c x

    member __.Run f = fun x -> (f <| fun _ -> true) x

    [<CustomOperation("eq", AllowIntoPattern = true)>]
    /// Equal
    member __.Equals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() = x && c x

    [<CustomOperation("ne", AllowIntoPattern = true)>]
    /// Not equal
    member __.NotEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <> x && c x

    [<CustomOperation("gt", AllowIntoPattern = true)>]
    /// Greater than
    member __.GreaterThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() > x && c x

    [<CustomOperation("lt", AllowIntoPattern = true)>]
    /// Less than
    member __.LessThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() < x && c x

    [<CustomOperation("ge", AllowIntoPattern = true)>]
    /// Greater then or equal
    member __.GreaterEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() >= x && c x

    [<CustomOperation("le", AllowIntoPattern = true)>]
    /// Less then or equal
    member __.LessEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <= x && c x

/// x |> batchOr { eq a; ne b; gt c; lt d; ge e; le f; }
///
/// is equivalent to
///
/// x = a || x <> b || x > c || x < d || x >= e || x <= f
let batchOr = BatchOrBuilder()
/// x |> batchAnd { eq a; ne b; gt c; lt d; ge e; le f; }
///
/// is equivalent to
///
/// x = a && x <> b && x > c && x < d && x >= e && x <= f
let batchAnd = BatchAndBuilder()
