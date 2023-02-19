using UnityEngine;
using Game.Operator;

public class AudioPlayer : MonoBehaviour
{
	#region Set this class to singleton
	static AudioPlayer _i; public static AudioPlayer i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<AudioPlayer>();
			}
			return _i;
		}
	}
	#endregion

   	public AudioClip eatAudio, hurtAudio, overAudio, upgradeAudio, outMoneyAudio, spawnAudio, tailAudio;

	public void Play(AudioClip audio)
	{
		//Play given audio
		SessionOperator.i.audios.soundSource.PlayOneShot(audio);
	}
}