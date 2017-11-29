package a.quiz;

import org.apache.commons.lang3.StringEscapeUtils;
import org.json.JSONException;
import org.json.JSONObject;

import static org.apache.commons.lang3.StringEscapeUtils.escapeHtml4;
import static org.apache.commons.lang3.StringEscapeUtils.unescapeHtml4;


/**
 * Created by Liam on 23-Nov-17.
 */

class Question
{
    String category;
    String difficulty;
    String question;
    String correct_answer;
    String[] incorrect_answers;
/*
    [{"category":"General Knowledge","type":"multiple","difficulty":"easy",
        "question":"Virgin Trains, Virgin Atlantic and Virgin Racing, are all companies owned by which famous entrepreneur?  " +
        " ","correct_answer":"Richard Branson","incorrect_answers":["Alan Sugar","Donald Trump","Bill Gates"]}
        */

    public Question(String category, String difficulty, String question, String correct_answer, String[] incorrect_answers)
    {
        this.category = category;
        this.difficulty = difficulty;
        this.question = question;
        this.correct_answer = correct_answer;
        this.incorrect_answers = incorrect_answers;
    }


    // Create question from JSONObject
    public static Question FromJson(JSONObject jsonObject)
    {


        try
        {
            String category = jsonObject.getString("category");
            String difficulty = jsonObject.getString("difficulty");
            String question = unescapeHtml4(jsonObject.getString("question"));
            String correct_answer = unescapeHtml4(jsonObject.getString("correct_answer"));
            String[] incorrect_answers = new String[3];
            incorrect_answers[0] = unescapeHtml4(jsonObject.getJSONArray("incorrect_answers").getString(0));
            incorrect_answers[1] = unescapeHtml4(jsonObject.getJSONArray("incorrect_answers").getString(1));
            incorrect_answers[2] = unescapeHtml4(jsonObject.getJSONArray("incorrect_answers").getString(2));

            // Create object
            return new Question(category, difficulty, question, correct_answer, incorrect_answers);

        } catch (JSONException e)
        {
            e.printStackTrace();
        }

        return null;
    }

}
