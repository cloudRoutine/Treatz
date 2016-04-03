﻿module PathFinding

  open System.Collections.Generic
  open System.Linq
  open TreatzGame


  
  let createInitialFrontier state : HashSet<Node> =
    let q = HashSet<Node>()
    q.Add(state) |> ignore
    q

  let expandNode (node:Node) = 
    node.Neighbours 

    // not the most efficient way to calc distance but ...
  let calcDistance (node:Node) goalNode =
    
    let x = (node.Identity.X - goalNode.Identity.X) ** 2.0
    let y = ( node.Identity.Y - goalNode.Identity.Y) ** 2.0
    sqrt( x + y )

  let search startNode goal: Node array =
    let frontier = createInitialFrontier startNode 
    let explored = new HashSet<Node>()
    if (frontier.Count = 0) then 
        printfn "if count %A" frontier.Count
        [||]
    else  
      while frontier.Count > 0 do      
        let currentNode = 
            frontier.ToArray() 
            |> Seq.minBy(fun x -> 1.0 + calcDistance x goal)
        frontier.Remove(currentNode)  |> printfn "Removed from frontier %A "    
        if (currentNode.Identity = goal.Identity) then
          printfn "explored and goal match %A" currentNode.Identity
        else
          explored.Add(currentNode) |> printfn "Added to explored %A "    
        
          (expandNode currentNode)
          |> Seq.iter(fun n -> 
                if not( explored.Contains(n)) then frontier.Add(n) |> printfn "Added to frontier %A" )
          printfn "final %A" currentNode.Identity

      explored.ToArray()

