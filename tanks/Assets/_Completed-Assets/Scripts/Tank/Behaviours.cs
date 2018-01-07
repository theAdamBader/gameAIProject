/*
These are 3 cases of my own behaviours of the AI and a default which was teaked from Jeremy Gow's TrackBehaviour AI

REFERENCE:
Jeremy Gow's Tanks! Behaviour Tree AI - modified scripts File (Track Behaviour code): https://learn.gold.ac.uk/course/view.php?id=6843&section=2
NP Behave Unity List: http://unitylist.com/r/69f/np-behave

*/

using UnityEngine;
using NPBehave;
using System.Collections.Generic;

namespace Complete
{
	/*
    Example behaviour trees for the Tank AI.  This is partial definition:
    the core AI code is defined in TankAI.cs.

    Use this file to specifiy your new behaviour tree.
     */
	public partial class TankAI : MonoBehaviour
	{
		private Root CreateBehaviourTree() {

			switch (m_Behaviour) {

			case 1:
				return SimpleBehaviour(); 
			case 2:
				return BetterBehaviour();
			case 3:
				return UnpredictableBehaviour();

			default:
				return new Root (SuchFun());//this Root calls the default case, SuchFun
			}
		}

		/* Actions */
		private Node  StopMoving(){
			return new Action(() => Move(0)); //This would stop the movement of the AI
		}

		private Node StopTurning() {
			return new Action(() => Turn(0));//This would stop the turning of the AI
		}

		private Node RandomFire() {
			return new Action(() => Fire(UnityEngine.Random.Range(0.0f, 1.0f)));//This would randomly fire at the target
		}

		//SuchFun is case 0 (Fun)
		//this is a private node that contains the selectors for the default case
		//it has an interval of 0.2 and calls UpdatePerception 
		//first blackboard uses the of centre which once it tracks the target it stops turns and randomly fires
		//second blackboard checks if the target is at 10, if so then it tracks you but moves really slow, giving the target time to dash away from the enemy
		//third blackboard checks the x axis of the plane to track where the tank is heading to, if right then turns; else it turns left
		private Node SuchFun(){
			return new Service(0.15f, UpdatePerception,
				new Selector(
					new BlackboardCondition("targetOffCentre",
						Operator.IS_SMALLER_OR_EQUAL, 0.1f,
						Stops.IMMEDIATE_RESTART,
						new Sequence(StopTurning(),//this child node allows to stop the turn and fire random from 1 to -1
							new Wait(0.5f),
							RandomFire())),//this function is called from the Node Random Fire
					
					new BlackboardCondition("targetDistance",
						Operator.IS_SMALLER_OR_EQUAL, 10.0f,//if target is 10 pixels near the enemy then enemy moves
						Stops.IMMEDIATE_RESTART,

						new Action(() => Move(0.2f))),//moves forward towards the enemy
					
					new BlackboardCondition("targetOnRight",
						Operator.IS_EQUAL, true,//if the targetOnRight is true then turn right towards the target
						Stops.IMMEDIATE_RESTART,
						new Action(() => Turn(0.2f))),
					
					new Action(() => Turn(-0.2f))//else it will turn left towards the target
				)
			);
		}

		//SimpleBehaviour is case 1 
		//Selector randomly selects one of the blackboard conditions
		//First blackboard decides if the enemy is behind and following the squences it will stop, wait then turn
		//Second blackboard detects if the player is in front then it stops
		//Third blackboard detects if the player is greater than 10, if so it moves towards it
		//Fourth condition detects is the player is less or equal than 10
		//Fifth condition detects the player's x axis and follows where it moves
		//Last turns the opposite direction
		private Root SimpleBehaviour()
		{
			return new Root(
				new Service(0.2f, UpdatePerception,
					new Selector(
						
						new BlackboardCondition("targetInFront",
							Operator.IS_EQUAL, false,
							Stops.IMMEDIATE_RESTART,

							new Sequence(StopMoving(),
								new Wait(0.5f),
								new Action(() => Turn(1.0f)))),

							new BlackboardCondition("targetOffCentre",
								Operator.IS_SMALLER_OR_EQUAL, 0.1f,
								Stops.LOWER_PRIORITY_IMMEDIATE_RESTART,
							StopTurning()),
						
							new BlackboardCondition("targetDistance",
								Operator.IS_GREATER, 10.0f,
								Stops.LOWER_PRIORITY,
							new Action(() => Move(0.6f))),


						new BlackboardCondition("targetDistance",
							Operator.IS_SMALLER_OR_EQUAL, 10.0f,
							Stops.IMMEDIATE_RESTART,
							new Sequence( StopMoving(),
								new Wait (0.1f),
								RandomFire())),


							new BlackboardCondition("targetOnRight",
								Operator.IS_EQUAL, true,
								Stops.IMMEDIATE_RESTART,
								new Action(() => Turn(0.6f))),

							new Action(() => Turn(-0.6f))
						
					)
				)
			);
		}

		//BetterBehaviour is case 2
		//First blackboard decides if the enemy is behind and following the squences it will stop, wait then turn
		//Second activates at random, 1%, which will reverse the AI and shoot while reversing 
		//Third blackboard detects player and stops turning
		//Forth condition detects the player's x axis and follows where it moves
		//Fifth condition detects player if less than the 20 and follows it
		//Last turns the opposite direction
		private Root BetterBehaviour()
		{
			return new Root(
				new Service(0.2f, UpdatePerception,
					new Selector(

						new BlackboardCondition("targetInFront",
							Operator.IS_EQUAL, false,
							Stops.BOTH,
					
							new Sequence(StopMoving(),
								new Wait(0.2f),
								new Action(() => Turn(0.5f)))),

						new NPBehave.Random(0.01f,new BlackboardCondition("targetInFront",
							Operator.IS_EQUAL, true,
							Stops.SELF,

							new Sequence(new Action(() => Move(-0.1f)),
								new Wait(0.5f),
								new Action(() => Turn(-0.1f)),
								RandomFire()))),

						new BlackboardCondition("targetOffCentre",
							Operator.IS_SMALLER_OR_EQUAL, 0.1f,
							Stops.LOWER_PRIORITY_IMMEDIATE_RESTART,
							StopTurning()),

						new BlackboardCondition("targetOnRight",
							Operator.IS_EQUAL, true,
							Stops.IMMEDIATE_RESTART,
							new Action(() => Turn(1.0f))),
						
						new BlackboardCondition("targetDistance",
							Operator.IS_SMALLER_OR_EQUAL, 20.0f,
							Stops.LOWER_PRIORITY,
							new Sequence(new Action(() => Move(0.6f)),
								new Wait(1.0f),
								RandomFire())),
				
						new Action(() => Turn(-1.0f))
					)
				)
			);
		}

		//UnpredictableBehaviour is case 3 
	
		private Root UnpredictableBehaviour()
		{
			return new Root(
				new Selector(
				new Service(0.5f, UpdatePerception,
					new Selector(
						
					
						
						)		
					)
				)
			);
		}

		private void UpdatePerception() {
			Vector3 targetPos = TargetTransform().position;
			Vector3 localPos = this.transform.InverseTransformPoint(targetPos);
			Vector3 heading = localPos.normalized;
			blackboard["targetDistance"] = localPos.magnitude;
			blackboard["targetInFront"] = heading.z > 0;
			blackboard["targetOnRight"] = heading.x > 0;
			blackboard["targetOffCentre"] = Mathf.Abs(heading.x);
		}
	}
}