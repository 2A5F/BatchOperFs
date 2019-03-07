namespace BatchOper.BreakOrContinue

type BreakOrContinue =
/// The break statement terminates the closest enclosing loop
| Break
/// The continue statement passes control to the next iteration of the enclosing loop statement in which it appears.
| Continue

type BreaktoOrContinueto = 
/// The break statement terminates the closest enclosing loop
| Breakto of string
/// The continue statement passes control to the next iteration of the enclosing loop statement in which it appears.
| Continueto of string