namespace Iago.Language
open System.Reflection

module Keywords =
    type Action = delegate of unit -> unit
    type Describe = {
        Title :string
        Action : Action
    } 
    type It = It of Action
    
    
    
    
    
    
