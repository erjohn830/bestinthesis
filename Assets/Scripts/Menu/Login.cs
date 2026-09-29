using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class FirebaseAuthManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    public TMP_InputField regEmail;
    public TMP_InputField regPassword;
    public TMP_InputField conPassword;

    public GameObject loginPanel;
    public GameObject registerPanel;

    FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase Ready");
            }
            else
            {
                Debug.LogError("Firebase Error: " + task.Result);
            }
        });

        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    public async void Login()
    {
        AuthResult result = await auth.SignInWithEmailAndPasswordAsync(emailInput.text, passwordInput.text);
        FirebaseUser user = result.User;

        if (user != null)
        {
            Debug.Log("Login Success");
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("Login Failed");
        }
    }

    public async void Register()
    {
        if (regPassword.text == conPassword.text)
        {
            var result = await auth.CreateUserWithEmailAndPasswordAsync(regEmail.text, regPassword.text);
            Debug.Log("Register Success");

            // GO BACK TO LOGIN PANEL
            registerPanel.SetActive(false);
            loginPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Password not match");
        }
    }

    public void OpenRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void BackToLogin()
    {
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
    }
}