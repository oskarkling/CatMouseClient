using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();

    public ushort Id { get; private set; }
    public bool IsLocal { get; private set; }
    private bool IsMoving;
    private Vector3 targetPosition;

    [SerializeField] private Transform camTransform;

    private string username;

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    public void Update()
    {
        if(IsMoving)
        {
            // To smooth out movement
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
            if(transform.position == targetPosition)
            {
                IsMoving = false;
            }
        }
    }

    private void Move(Vector3 newPosition, Vector3 forward)
    {
        //transform.position = newPosition;

        IsMoving = true;
        targetPosition = newPosition;
        

        if(!IsLocal)
        {
            camTransform.forward = forward;
        }
    }

    public static void Spawn(ushort id, string username, Vector3 position)
    {
        Player player;
        
        if(id == NetworkManager.Singleton.Client.Id)
        {
            player = Instantiate(GameLogic.Singleton.LocalPlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
            player.IsLocal = true;
        }
        else
        {
            player = Instantiate(GameLogic.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
            player.IsLocal = false;
        }

        player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
        player.Id = id;
        player.username = username;

        list.Add(id, player);
    }

    // Handles messages from server to cleint
    [MessageHandler((ushort)ServerToClientId.playerSpawned)]
    private static void SpawnPlayer(Message message)
    {
        Spawn(message.GetUShort(), message.GetString(), message.GetVector3());
    }

    [MessageHandler((ushort)ServerToClientId.playerMovement)]
    private static void playerMovement(Message message)
    {
        if(list.TryGetValue(message.GetUShort(), out Player player))
        {
            player.Move(message.GetVector3(), message.GetVector3());
        }
    }
}
