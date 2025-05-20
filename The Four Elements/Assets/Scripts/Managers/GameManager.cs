using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   private EntityAttackManager _attackManager;
   private EntityHitManager _entityHitManager;
   [SerializeField] private float time;
   [SerializeField] private float scale;
   private bool _isHitStopping = false;
   public string sceneToLoadName = "MainMenu";
   
   private void Awake()
   {
      _attackManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityAttackManager>();
      _entityHitManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityHitManager>();
   }

   private void OnEnable()
   {
      _attackManager.onAttackHit += StartTimeChange;
      _entityHitManager.OnEntityDied += Death;
   }

   private void OnDisable()
   {
      _attackManager.onAttackHit -= StartTimeChange;
      _entityHitManager.OnEntityDied -= Death;
   }

   private void Death()
   {
      StartCoroutine(DeathSequence());
   }
   private IEnumerator DeathSequence()
   {
      float lerpDuration = 1f; // Adjust to control how fast time slows down
      float elapsed = 0f;
      float startScale = Time.timeScale;

      while (elapsed < lerpDuration)
      {
         elapsed += Time.unscaledDeltaTime;
         Time.timeScale = Mathf.Lerp(startScale, 0f, elapsed / lerpDuration);
         Time.fixedDeltaTime = 0.02f * Time.timeScale;
         yield return null;
      }

      Time.timeScale = 0f;
      Time.fixedDeltaTime = 0f;

      // Enable cursor for menu interaction
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;

      // Begin loading the scene after time has fully stopped
      LoadSceneAsync();
   }

   void StartTimeChange(GameObject obj , Vector3 p)
   {
      Stop(time , scale);
   }
   
   public void LoadSceneAsync()
   {
        
      StartCoroutine(LoadYourAsyncScene());
   }

   IEnumerator LoadYourAsyncScene()
   {
      // Show loading screen
      

      // Start loading the scene asynchronously
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoadName);

      // Don't activate the new scene until it's fully loaded and you allow it
      operation.allowSceneActivation = false;

      // Loop while the scene is not yet loaded
      while (!operation.isDone)
      {
       

         // Check if the scene is almost loaded (progress is near 0.9)
         if (operation.progress >= 0.9f)
         {
            // You can add a "Press any key to continue" or other logic here
            // For now, let's just activate it when it's ready.
            operation.allowSceneActivation = true;
         }

         yield return null; // Wait for the next frame
      }
      
   }

   public void Stop(float duration, float scale = 0f)
   {
      if (_isHitStopping) return;
      StartCoroutine(HitStopCoroutine(duration, scale));
   }

   private IEnumerator HitStopCoroutine(float duration, float scale)
   {
      _isHitStopping = true;

      float originalTimeScale = Time.timeScale;
      Time.timeScale = scale;
      Time.fixedDeltaTime = 0.02f * Time.timeScale;

      yield return new WaitForSecondsRealtime(duration);

      Time.timeScale = originalTimeScale;
      Time.fixedDeltaTime = 0.02f * originalTimeScale;
      _isHitStopping = false;
   }
   
}
