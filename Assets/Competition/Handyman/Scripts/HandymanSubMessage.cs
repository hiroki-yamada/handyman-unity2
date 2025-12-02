using UnityEngine;
using UnityEngine.EventSystems;
using SIGVerse.Common;
using SIGVerse.RosBridge;
using System.Collections.Generic;

namespace SIGVerse.Competition.Handyman
{
	public interface IRosMsgReceiveHandler : IEventSystemHandler
	{
		void OnReceiveRosMessage(RosBridge.handyman_msgs.msg.HandymanMsg handymanMsg);
	}

	public class HandymanSubMessage : RosSubMessage<RosBridge.handyman_msgs.msg.HandymanMsg>
	{
		public List<GameObject> destinations;

		protected override void SubscribeMessageCallback(RosBridge.handyman_msgs.msg.HandymanMsg handymanMsg)
		{
			SIGVerseLogger.Info("Received message :"+handymanMsg.message);

			foreach(GameObject destination in this.destinations)
			{
				ExecuteEvents.Execute<IRosMsgReceiveHandler>
				(
					target: destination,
					eventData: null,
					functor: (reciever, eventData) => reciever.OnReceiveRosMessage(handymanMsg)
				);
			}
		}
	}
}
