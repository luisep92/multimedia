package com.luiescpiq.proyecto

import android.content.Context
import android.view.ActionMode
import android.view.LayoutInflater
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.snackbar.Snackbar
import com.luiescpiq.proyecto.databinding.ItemGameListBinding
import com.squareup.picasso.Picasso

data class MyGame (val name: String, val genre: String, val description: String, val image: String, val score: Float, val id: String)

class GameAdapter (gameList: MutableList<MyGame>, context: Context): RecyclerView.Adapter<GameAdapter.DiscoViewHolder>() {
    var myGames: MutableList<MyGame>
    var myContext: Context
    private var actionMode: ActionMode? = null

    init {
        myGames = gameList
        myContext = context
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): DiscoViewHolder {
        val view = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.item_game_list, parent, false)
        return DiscoViewHolder(view)
    }
    override fun onBindViewHolder(holder: DiscoViewHolder, position: Int) {
        val item = myGames.get(position)
        holder.bind(item, myContext)
    }
    override fun getItemCount(): Int {
        return myGames.size
    }
    inner class DiscoViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        private val binding = ItemGameListBinding.bind(view)
        private lateinit var description: String
        private lateinit var id: String
        fun bind(game: MyGame, context: Context) {
            binding.txtNombre.text = game.name
            binding.txtGenero.text = game.genre
            Picasso.get().load(game.image).into(binding.image)
            binding.score.rating = game.score
            description = game.description
            id = game.id

            itemView.setOnClickListener {
               Toast.makeText(myContext, game.name, Toast.LENGTH_SHORT)
            }

            itemView.setOnLongClickListener {
                when (actionMode) {
                    null -> {
                        actionMode = it.startActionMode(actionModeCallback)
                        it.isSelected = true
                        true
                    }
                    else -> false
                }
            }
        }

        private val actionModeCallback = object : ActionMode.Callback {
            override fun onCreateActionMode(mode: ActionMode, menu: Menu): Boolean {
                val inflater = mode?.menuInflater
                inflater?.inflate(R.menu.action_menu, menu)
                return true
            }

            override fun onPrepareActionMode(mode: ActionMode, menu: Menu): Boolean {
                return false
            }

            override fun onActionItemClicked(mode: ActionMode, item: MenuItem): Boolean {
                return when (item.itemId) {
                    R.id.optionDelete -> {
                        Database.instance.deleteItem(id)
                        notifyItemRemoved(adapterPosition)
                        Snackbar.make(binding.root, "Eliminado ${binding.txtNombre.text} correctamente", Snackbar.LENGTH_SHORT).show()
                        return true
                    }
                    R.id.optionEdit -> {
                        Toast.makeText(myContext, "$id element ${adapterPosition}", Toast.LENGTH_LONG).show()
                        return true
                    }
                    else -> false
                }
            }

            override fun onDestroyActionMode(mode: ActionMode) {
                actionMode = null
            }
        }
    }
}