# Dynamic Blackboard System

## Overview

Implementation of a dynamic blackboard system, inspired by the architecture of *No One Lives Forever 2: A Spy in H.A.R.M.S Way*. Note: the implementation details were gleaned entirely [from a slideshow presentation](https://slideplayer.com/slide/6102412/) by game AI wizard, Jeff Orkin... but without access to the original talk itself. Some creative extrapolation was necessary on my part, and is very much so my interpretation of how to implement this in C# on a modern game engine.

According to Jeff Orkin, there are two kinds of blackboards: static, and dynamic. Static blackboards are pre-determined repositories of agent knowledge about the game state, and accessors and modifiers to write and read that data; target position, agent velocity, current health, etc. This type of knowledge is best for intra-agent coordination; for example, maybe an agent's vision sensor remembers the target position, and the agent's decision-making system can then determine its distance from the target, and in turn if it should then pursue or attack.

Alternatively, a dynamic blackboard is a dynamic repository of knowledge shared across agents, and is best for inter-agent coordination. Rather than store references of the game state directly, dynamic blackboard records carry information *about* the game state. For instance, if an agent could record that it executed a specific action in the blackboard, and another agent could query the blackboard and then choose not to perform that same action. Or perhaps agents could use this system to compare pathing destinations, so they don't end up at the same exact location (this example would require a few other sub-system to all come together, where the pathing system would need to dynamically increase the cost of the area of the navmesh where the agent is pathing to). The system can also be used to achieve more complex reasoning; for example, an enemy soldier might retreat to a sniper position if and only if there are no other active snipers. You get the idea.

To sum up, rather than have your agent's sub-systems for animation, navigation, sensors, decision-making, etc. all tightly coupled up with each other, they can instead all just reference your blackboard.

## Implementation

Here is an actual piece of code from one of my game prototypes in the Unity game engine, called from an enemy agent's vision sensor upon sensing a target:

            globalBlackboard.RemoveRecords(BlackboardRecordType.TargetLastSeenTime);
            globalBlackboard.AddRecord(BlackboardRecordType.TargetLastSeenTime, this.GetInstanceID(), target.GetInstanceID(), Time.time);

This lets my enemy agents coordinate with each other; when one agent sees the player, every active agent can be suddenly aware.

                if (globalBlackboard.TryGetRecordData(BlackboardRecordType.TargetLastSeenTime, out float[] values) && (Time.time - values[0]) < Time.deltaTime)
                    //code to have some messaging system send messages to every enemy agent's decision-making system
