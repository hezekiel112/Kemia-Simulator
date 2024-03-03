using System;
using System.Text;
using UnityEngine;

namespace KemiaSimulatorEnvironment.GameHandler {
    [Serializable]
    public struct GameBuildIdentity {
        public readonly byte MINOR_VERSION {
            get;
        }

        public readonly byte PATCH_VERSION {
            get;
        }

        public readonly byte MAJOR_VERSION {
            get;
        }

        public readonly int REVISION_NUMBER {
            get;
        }

        public readonly bool IS_ALPHA_OR_BETA_OR_UNIT_TESTING;

        public string GetSemanticVersionning() {
            return IS_ALPHA_OR_BETA_OR_UNIT_TESTING ? new StringBuilder($"(rev{REVISION_NUMBER}).{MAJOR_VERSION}x{MINOR_VERSION}x{PATCH_VERSION}").ToString() : new StringBuilder($"(unstable.rev{REVISION_NUMBER}).{REVISION_NUMBER}{MAJOR_VERSION}x{MINOR_VERSION}x{PATCH_VERSION}").ToString();
        }

        public GameBuildIdentity(byte majorVersion, byte minorVersion, byte patchVersion, int revisionNumber, bool isAlphaOrBetaOrUnitTesting = false) {
            MAJOR_VERSION = majorVersion;
            MINOR_VERSION = minorVersion;
            PATCH_VERSION = patchVersion;
            REVISION_NUMBER = revisionNumber;
            IS_ALPHA_OR_BETA_OR_UNIT_TESTING = isAlphaOrBetaOrUnitTesting;
#if UNITY_EDITOR
            Debug.Log(GetSemanticVersionning());
#endif
        }
    }
}