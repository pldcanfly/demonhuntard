/*
 * GrammarTokenizer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

/**
 * <remarks>A character stream tokenizer.</remarks>
 */
internal class GrammarTokenizer : Tokenizer {

    /**
     * <summary>Creates a new tokenizer for the specified input
     * stream.</summary>
     *
     * <param name='input'>the input stream to read</param>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    public GrammarTokenizer(TextReader input)
        : base(input, false) {

        CreatePatterns();
    }

    /**
     * <summary>Initializes the tokenizer by creating all the token
     * patterns.</summary>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    private void CreatePatterns() {
        TokenPattern  pattern;

        pattern = new TokenPattern((int) GrammarConstants.NUMBER,
                                   "NUMBER",
                                   TokenPattern.PatternType.REGEXP,
                                   "[0-9]+");
        AddPattern(pattern);

        pattern = new TokenPattern((int) GrammarConstants.CHARACTER,
                                   "CHARACTER",
                                   TokenPattern.PatternType.REGEXP,
                                   "[a-zA-Z]+");
        AddPattern(pattern);

        pattern = new TokenPattern((int) GrammarConstants.WHITESPACE,
                                   "WHITESPACE",
                                   TokenPattern.PatternType.REGEXP,
                                   "[ \\t\\n\\r]+");
        AddPattern(pattern);

        pattern = new TokenPattern((int) GrammarConstants.EVERYTHING,
                                   "EVERYTHING",
                                   TokenPattern.PatternType.REGEXP,
                                   ".");
        AddPattern(pattern);
    }
}
