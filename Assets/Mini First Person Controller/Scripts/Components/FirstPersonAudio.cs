using System.Linq;
using UnityEngine;

namespace KemiaSimulatorEnvironment.Player {
    public class FirstPersonAudio : MonoBehaviour {
        [Header("Setup")]
        [SerializeField] FirstPersonMovement _character;
        [SerializeField] GroundCheck _groundCheck;

        [Header("Step")]
        [SerializeField] AudioSource _stepAudio;
        [SerializeField] AudioSource _runningAudio;
        [Tooltip("Minimum velocity for moving audio to play")]
        /// <summary> "Minimum velocity for moving audio to play" </summary>

        [Header("Landing")]
        [SerializeField] AudioSource landingAudio;
        [SerializeField] AudioClip[] landingSFX;

        [Header("Jump")]
        [SerializeField] Jump _jump;
        [SerializeField] AudioSource _jumpAudio;
        [SerializeField] AudioClip[] _jumpSFX;

        [Header("Crouch")]
        [SerializeField] Crouch _crouch;
        [SerializeField] AudioSource _crouchStartAudio, _crouchedAudio, _crouchEndAudio;
        [SerializeField] AudioClip[] _crouchStartSFX, _crouchEndSFX;

        Vector2 LastCharacterPosition {
            get;
            set;
        }

        Vector2 CurrentCharacterPosition => new Vector2(_character.transform.position.x, _character.transform.position.z);
        AudioSource[] MovingAudios => new AudioSource[] { _stepAudio, _runningAudio, _crouchedAudio };
        readonly float VelocityThresold = .01f;


        void Reset() {
            // Setup stuff.
            _character = GetComponentInParent<FirstPersonMovement>();
            _groundCheck = (transform.parent ?? transform).GetComponentInChildren<GroundCheck>();
            _stepAudio = GetOrCreateAudioSource("Step Audio");
            _runningAudio = GetOrCreateAudioSource("Running Audio");
            landingAudio = GetOrCreateAudioSource("Landing Audio");

            // Setup jump audio.
            _jump = GetComponentInParent<Jump>();
            if (_jump) {
                _jumpAudio = GetOrCreateAudioSource("Jump audio");
            }

            // Setup crouch audio.
            _crouch = GetComponentInParent<Crouch>();
            if (_crouch) {
                _crouchStartAudio = GetOrCreateAudioSource("Crouch Start Audio");
                _crouchStartAudio = GetOrCreateAudioSource("Crouched Audio");
                _crouchStartAudio = GetOrCreateAudioSource("Crouch End Audio");
            }
        }

        void OnEnable() => SubscribeToEvents();

        void OnDisable() => UnsubscribeToEvents();

        void FixedUpdate() {
            // Play moving audio if the character is moving and on the ground.
            float velocity = Vector3.Distance(CurrentCharacterPosition, LastCharacterPosition);
            if (velocity >= VelocityThresold && _groundCheck && _groundCheck.isGrounded) {
                if (_crouch && _crouch.IsCrouched) {
                    SetPlayingMovingAudio(_crouchedAudio);
                }
                else if (_character.IsRunning) {
                    SetPlayingMovingAudio(_runningAudio);
                }
                else {
                    SetPlayingMovingAudio(_stepAudio);
                }
            }
            else {
                SetPlayingMovingAudio(null);
            }

            // Remember lastCharacterPosition.
            LastCharacterPosition = CurrentCharacterPosition;
        }


        /// <summary>
        /// Pause all MovingAudios and enforce play on audioToPlay.
        /// </summary>
        /// <param name="audioToPlay">Audio that should be playing.</param>
        void SetPlayingMovingAudio(AudioSource audioToPlay) {
            // Pause all MovingAudios.
            foreach (var audio in MovingAudios.Where(audio => audio != audioToPlay && audio != null)) {
                audio.Pause();
            }

            // Play audioToPlay if it was not playing.
            if (audioToPlay && !audioToPlay.isPlaying) {
                audioToPlay.Play();
            }
        }

        #region Play instant-related audios.
        void PlayLandingAudio() => PlayRandomClip(landingAudio, landingSFX);
        void PlayJumpAudio() => PlayRandomClip(_jumpAudio, _jumpSFX);
        void PlayCrouchStartAudio() => PlayRandomClip(_crouchStartAudio, _crouchStartSFX);
        void PlayCrouchEndAudio() => PlayRandomClip(_crouchEndAudio, _crouchEndSFX);
        #endregion

        #region Subscribe/unsubscribe to events.
        void SubscribeToEvents() {
            // PlayLandingAudio when Grounded.
            _groundCheck.Grounded += PlayLandingAudio;

            // PlayJumpAudio when Jumped.
            if (_jump) {
                _jump.Jumped += PlayJumpAudio;
            }

            // Play crouch audio on crouch start/end.
            if (_crouch) {
                _crouch.CrouchStart += PlayCrouchStartAudio;
                _crouch.CrouchEnd += PlayCrouchEndAudio;
            }
        }

        void UnsubscribeToEvents() {
            // Undo PlayLandingAudio when Grounded.
            _groundCheck.Grounded -= PlayLandingAudio;

            // Undo PlayJumpAudio when Jumped.
            if (_jump) {
                _jump.Jumped -= PlayJumpAudio;
            }

            // Undo play crouch audio on crouch start/end.
            if (_crouch) {
                _crouch.CrouchStart -= PlayCrouchStartAudio;
                _crouch.CrouchEnd -= PlayCrouchEndAudio;
            }
        }
        #endregion

        #region Utility.
        /// <summary>
        /// Get an existing AudioSource from a name or create one if it was not found.
        /// </summary>
        /// <param name="name">Name of the AudioSource to search for.</param>
        /// <returns>The created AudioSource.</returns>
        AudioSource GetOrCreateAudioSource(string name) {
            // Try to get the audiosource.
            AudioSource result = System.Array.Find(GetComponentsInChildren<AudioSource>(), a => a.name == name);
            if (result)
                return result;

            // Audiosource does not exist, create it.
            result = new GameObject(name).AddComponent<AudioSource>();
            result.spatialBlend = 1;
            result.playOnAwake = false;
            result.transform.SetParent(transform, false);
            return result;
        }

        static void PlayRandomClip(AudioSource audio, AudioClip[] clips) {
            if (!audio || clips.Length <= 0)
                return;

            // Get a random clip. If possible, make sure that it's not the same as the clip that is already on the audiosource.
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            if (clips.Length > 1)
                while (clip == audio.clip)
                    clip = clips[Random.Range(0, clips.Length)];

            // Play the clip.
            audio.clip = clip;
            audio.Play();
        }
        #endregion
    }

}