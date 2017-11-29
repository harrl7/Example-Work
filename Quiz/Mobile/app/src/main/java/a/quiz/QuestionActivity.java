package a.quiz;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.support.v4.content.res.ResourcesCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.JsonReader;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class QuestionActivity extends AppCompatActivity
{

    // Layout
    TextView tvQuestion;
    Button[] btnAns;
    Button btnNext;
    ImageView[] ivMark;

    // Questions JSON
    JSONArray questionJson;
    String jsonString;

    // Question
    Question question;
    int questionIndex;
    int correctAnswerIndex;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_question);

        jsonString = getIntent().getStringExtra("json");
        //JSONArray questionJson = JSONArray();

        // Question TextView
        tvQuestion = (TextView) findViewById(R.id.tvQuestion);


        // Answer Buttons
        btnAns = new Button[4];

        btnAns[0] = (Button) findViewById(R.id.btnAns0);
        btnAns[1] = (Button) findViewById(R.id.btnAns1);
        btnAns[2] = (Button) findViewById(R.id.btnAns2);
        btnAns[3] = (Button) findViewById(R.id.btnAns3);

        btnAns[0].setOnClickListener(new AnswerButtonClickHandler());
        btnAns[1].setOnClickListener(new AnswerButtonClickHandler());
        btnAns[2].setOnClickListener(new AnswerButtonClickHandler());
        btnAns[3].setOnClickListener(new AnswerButtonClickHandler());


        // Marking image
        ivMark = new ImageView[4];

        ivMark[0] = findViewById(R.id.ivMark0);
        ivMark[1] = findViewById(R.id.ivMark1);
        ivMark[2] = findViewById(R.id.ivMark2);
        ivMark[3] = findViewById(R.id.ivMark3);


        // Next button
        btnNext = findViewById(R.id.btnNext);
        btnNext.setOnClickListener(new NextQuestionButtonClickHandler());

        btnNext.callOnClick();

       // question = nextQuestion();


    //    displayQuestion();
    }


    // Answer Button click handler
    public class AnswerButtonClickHandler implements View.OnClickListener
    {
        @Override
        public void onClick(View view)
        {
            // Find index of users selected answer
            int selectionIndex = -1;

            if(view.equals(btnAns[0])) selectionIndex = 0;
            if(view.equals(btnAns[1])) selectionIndex = 1;
            if(view.equals(btnAns[2])) selectionIndex = 2;
            if(view.equals(btnAns[3])) selectionIndex = 3;

            // disable answer buttons


            // Show next button
            btnNext.setVisibility(View.VISIBLE);

            // Mark image user selection
            ivMark[selectionIndex].setVisibility(View.VISIBLE);
            ivMark[selectionIndex].setImageDrawable(ResourcesCompat.getDrawable(getResources(), R.drawable.wrong, null));

            // Mark image correct answer
            ivMark[correctAnswerIndex].setVisibility(View.VISIBLE);
            ivMark[correctAnswerIndex].setImageDrawable(ResourcesCompat.getDrawable(getResources(), R.drawable.correct, null));
        }
    }


    // Next question Button click handler
    public class NextQuestionButtonClickHandler implements View.OnClickListener
    {
        @Override
        public void onClick(View view)
        {
            // Load next question
            question = nextQuestion();

            // End after final question
            if(question == null)
            {
                finish();
            }
            else
            {
                displayQuestion();
            }
        }
    }


    // Load next question
    private Question nextQuestion()
    {
        // Extract Question from JSOn
        try
        {

            JSONArray jsonArray = new JSONArray(jsonString);
            Question question = Question.FromJson(jsonArray.getJSONObject(questionIndex++));

            return question;

        } catch (JSONException e) { e.printStackTrace(); }
        catch (NullPointerException e) { e.printStackTrace(); }

        return null;
    }


    // Display question and answers on layout
    private void displayQuestion()
    {
        correctAnswerIndex = (int) (Math.random() * 4);
        //correctAnswerIndex = 2;

        // Question
        tvQuestion.setText(question.question);

        // Answer buttons
        int ansTick = correctAnswerIndex;
        btnAns[ansTick++ % 4].setText(question.correct_answer);
        btnAns[ansTick++ % 4].setText(question.incorrect_answers[0]);
        btnAns[ansTick++ % 4].setText(question.incorrect_answers[1]);
        btnAns[ansTick++ % 4].setText(question.incorrect_answers[2]);

        // Enable buttons

        // Hide next button
        btnNext.setVisibility(View.INVISIBLE);

        // Hide mark image
        ivMark[0].setVisibility(View.INVISIBLE);
        ivMark[1].setVisibility(View.INVISIBLE);
        ivMark[2].setVisibility(View.INVISIBLE);
        ivMark[3].setVisibility(View.INVISIBLE);
    }

}
