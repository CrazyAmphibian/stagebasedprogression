using BepInEx;
//using R2API;
using RoR2;
using UnityEngine;
//using UnityEngine.AddressableAssets;

namespace StageProgression
{

	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]

	public class Plugin : BaseUnityPlugin
	{
		public const string PluginGUID = PluginAuthor + "." + PluginName;
		public const string PluginAuthor = "CrazyAmphibian";
		public const string PluginName = "StageProgression";
		public const string PluginVersion = "1.3.0";

		public void Awake()
		{
			Log.Init(Logger);
			

			On.RoR2.Run.RecalculateDifficultyCoefficent += (orig, self) =>
			{
				
				DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(self.selectedDifficulty);

				float diffscaling = difficultyDef.scalingValue;
				int stageclears = self.stageClearCount;
				int playercount = self.participatingPlayerCount;
				float coeff = 1f;
				coeff += 1.25f*diffscaling*.5f/3f; //add an additional 1.25 blocks. this makes maps a bit harder when you first spawn to account for the difficulty never scaling up.

				float playerfactor = (float)playercount * .5f + .5f; //increased player scaling from .3x to .5x per player.
				//float stagefactor = (float)stageclears*1.5f;
				float stagefactor = (1f+0.025f*((float)stageclears-1f))*(float)stageclears*1.25f; //for each stage after the first, add 2.5% to the difficulty.

				coeff += stagefactor * diffscaling * playerfactor/3f; //no time here, boss!
				//we have to divide by 3 here because 1 coeff is the big bars, so this makes it the smaller bars
				//these settings get it so that you get 2.5 "blocks" every stage (plus a bonus 2.5% after more stages)
				coeff = Mathf.Min(coeff, (float)Run.ambientLevelCap);
				self.difficultyCoefficient = coeff;
				self.compensatedDifficultyCoefficient = coeff;

				self.ambientLevel = 1 + (coeff - playerfactor) / .33f;
				int ambientLevelFloor = Mathf.FloorToInt(self.ambientLevel);

				if (ambientLevelFloor != 0 && self.ambientLevelFloor > ambientLevelFloor) //check for ambient leveling up.
				{
					self.OnAmbientLevelUp();
				}

				self.ambientLevelFloor = ambientLevelFloor;

				//Log.Debug("$$$$ = StageProgression calculated some shit::" + diffscaling.ToString() +"|"+ stageclears.ToString() + "|" + playercount.ToString());
			};
			
		}
	}


}