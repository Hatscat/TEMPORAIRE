/*
Ultimate FPS Integration Example (shallow)

This simple example uses Dialogue System features to suspend UFPS gameplay and interact with an 
NPC to start a conversation.

(For a larger example that uses UFPS features to do the same, see the Deep Integration Example.)

The scene uses the basic UFPS Camera&Controller, but you could use the AdvancedPlayer rig instead.


On the Camera&Controller, these Dialogue System components were added:

- Show Cursor On Conversation: Unlocks the cursor during conversations so the player can use the
  response menu.
  
- Set Enabled On Dialogue Event: Disables all UFPS components on Camera&Controller and 
  Camera&Controller/FPSCamera during conversations, and re-enables when the conversation ends. 
  This prevents the player from walking away or taking control of the camera during conversations.
  
- Selector (on FPSCamera): Allows the player to interact with the NPC.


On the NPC, Private Hart, a child object named "AI" contains these components:

- Usable: Allows the player's Selector component to target the NPC.

- Conversation Trigger: Starts the conversation when the player uses the selector on the NPC.

*/