/*
 * GrammarParser.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

/**
 * <remarks>A token stream parser.</remarks>
 */
internal class GrammarParser : RecursiveDescentParser {

    /**
     * <summary>An enumeration with the generated production node
     * identity constants.</summary>
     */
    private enum SynteticPatterns {
    }

    /**
     * <summary>Creates a new parser with a default analyzer.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    public GrammarParser(TextReader input)
        : base(input) {

        CreatePatterns();
    }

    /**
     * <summary>Creates a new parser.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <param name='analyzer'>the analyzer to parse with</param>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    public GrammarParser(TextReader input, GrammarAnalyzer analyzer)
        : base(input, analyzer) {

        CreatePatterns();
    }

    /**
     * <summary>Creates a new tokenizer for this parser. Can be overridden
     * by a subclass to provide a custom implementation.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <returns>the tokenizer created</returns>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    protected override Tokenizer NewTokenizer(TextReader input) {
        return new GrammarTokenizer(input);
    }

    /**
     * <summary>Initializes the parser by creating all the production
     * patterns.</summary>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    private void CreatePatterns() {
        ProductionPattern             pattern;
        ProductionPatternAlternative  alt;

        pattern = new ProductionPattern((int) GrammarConstants.DOCUMENT,
                                        "DOCUMENT");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) GrammarConstants.TEXT, 1, -1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) GrammarConstants.TEXT,
                                        "TEXT");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GrammarConstants.EVERYTHING, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GrammarConstants.CHARACTER, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GrammarConstants.NUMBER, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GrammarConstants.WHITESPACE, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);
    }
}
