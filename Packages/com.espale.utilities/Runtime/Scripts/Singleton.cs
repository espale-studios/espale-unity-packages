using UnityEngine;
using UnityEngine.SceneManagement;

namespace Espale.Utilities
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		protected static T instance;

		protected void Awake()
		{
			if (instance)
			{
				BetterDebug.LogWarning($"Found a duplicate singleton ({gameObject.name}), destroying the duplicate.");
				Destroy(gameObject);
				return;
			}

			instance = this as T;
			DontDestroyOnLoad(gameObject);
			
			OnSceneChange(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
		}

		protected void OnEnable() => SceneManager.activeSceneChanged += OnSceneChange;
		protected void OnDisable() => SceneManager.activeSceneChanged -= OnSceneChange;
		
		/// <summary>
		/// Called when the scene changes, useful for updating scene references as they will be lost
		/// when the active scene changes.
		/// </summary>
		/// <param name="current">The current scene (old)</param>
		/// <param name="next">The next scene (new)</param>
		protected virtual void OnSceneChange(Scene current, Scene next) { }

		/// <summary>
		/// Returns the instance of the Singleton, if it doesn't exist, creates a new one using <c>Resources.Load</c>
		/// with the <c>prefabResourceName</c> parameter.
		/// </summary>
		/// <returns>Active instance</returns>
		protected static T GetInstance()
		{
			if (!instance)
				BetterDebug.LogError($"There is no instance of {typeof(T).Name}");
			
			return instance;
		}
	}
}
