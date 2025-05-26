using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public Sprite[] diceFaces; 
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private bool isRolling = false; // 

    void Start() // One time
    {
        animator.Play("Rolling S-6"); // rulle animation
        isRolling = true; // skifter boolean til at være sandt for at linje 16 går igang

        StartCoroutine(StopAndShowResult()); // sikrer at det kommer til at stoppe efter noget styk tid
    }

    private System.Collections.IEnumerator StopAndShowResult() 
    {
        yield return new WaitForSeconds(4.7f); // stopper Coroutinen efter 4,6 sekunder

        animator.enabled = false; // Stop animation
        isRolling = false; // booleanen går tilbage til at være falsk så linje 16 ikke går i gang igen

        int randomIndex = Random.Range(0, diceFaces.Length); 
        spriteRenderer.sprite = diceFaces[randomIndex]; // vælger en tilfældig side som inde på unity animationen
    }
}
