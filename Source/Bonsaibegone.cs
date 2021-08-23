using HugsLib;
using HarmonyLib;
using Verse;
using System;

namespace Bonsaibegone {
    public class BonsaiBegone : ModBase {
        public override string ModIdentifier {
            get { return "BonsaiBegone"; }
        }
        public BonsaiBegone() {
        }

        //delegate void SuppressBonsaiDeathMessage_Postfix(ref bool __result, string tag, float minSecondsSinceLastShow);
        public static void SuppressBonsaiDeathMessage_Postfix(ref bool __result, string tag, float minSecondsSinceLastShow) {
            if (__result) {
                if (tag == "MessagePlantDiedOfRot-Plant_TreeBonsai") {
                    Log.Message("[BonsaiBegone] Suppressed rotting bonsai tree message");
                    __result = false;
                }
            }
        }
        public override void Initialize() {
            _ = HarmonyInst.Patch(
                AccessTools.Method(typeof(MessagesRepeatAvoider), "MessageShowAllowed"),
                null,
                new HarmonyMethod(typeof(BonsaiBegone), "SuppressBonsaiDeathMessage_Postfix")  { }
                //AccessTools.Method(typeof(BonsaiBegone), "SuppressBonsaiDeathMessage_Postfix") ) { }
                );
            Logger.Message("Initialised BonsaiBegone");
        }


    }
    /*
    // public static bool MessageShowAllowed(string tag, float minSecondsSinceLastShow);
    [HarmonyPatch(typeof(MessagesRepeatAvoider))]
    [HarmonyPatch("MessageShowAllowed")]
    public static class BonsaiBegone_PreOpen_Patch {
        [HarmonyPrefix]
        public static bool SuppressBonsaiDeathMessage(ref bool __result, string tag, float minSecondsSinceLastShow) {
            if (tag == "MessagePlantDiedOfRot-Plant_TreeBonsai") {
                Log.Message("Supressing bonsai rotting death message");
                __result = true; // change result
                return false; // skip original
            }
            return true;

        }

    }*/
}