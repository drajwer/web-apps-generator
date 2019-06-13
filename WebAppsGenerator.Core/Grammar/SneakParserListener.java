// Generated from SneakParser.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link SneakParser}.
 */
public interface SneakParserListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link SneakParser#compileUnit}.
	 * @param ctx the parse tree
	 */
	void enterCompileUnit(SneakParser.CompileUnitContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#compileUnit}.
	 * @param ctx the parse tree
	 */
	void exitCompileUnit(SneakParser.CompileUnitContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#file}.
	 * @param ctx the parse tree
	 */
	void enterFile(SneakParser.FileContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#file}.
	 * @param ctx the parse tree
	 */
	void exitFile(SneakParser.FileContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#classDefs}.
	 * @param ctx the parse tree
	 */
	void enterClassDefs(SneakParser.ClassDefsContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#classDefs}.
	 * @param ctx the parse tree
	 */
	void exitClassDefs(SneakParser.ClassDefsContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#classDef}.
	 * @param ctx the parse tree
	 */
	void enterClassDef(SneakParser.ClassDefContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#classDef}.
	 * @param ctx the parse tree
	 */
	void exitClassDef(SneakParser.ClassDefContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#body}.
	 * @param ctx the parse tree
	 */
	void enterBody(SneakParser.BodyContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#body}.
	 * @param ctx the parse tree
	 */
	void exitBody(SneakParser.BodyContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#properties}.
	 * @param ctx the parse tree
	 */
	void enterProperties(SneakParser.PropertiesContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#properties}.
	 * @param ctx the parse tree
	 */
	void exitProperties(SneakParser.PropertiesContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#property}.
	 * @param ctx the parse tree
	 */
	void enterProperty(SneakParser.PropertyContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#property}.
	 * @param ctx the parse tree
	 */
	void exitProperty(SneakParser.PropertyContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#annotations}.
	 * @param ctx the parse tree
	 */
	void enterAnnotations(SneakParser.AnnotationsContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#annotations}.
	 * @param ctx the parse tree
	 */
	void exitAnnotations(SneakParser.AnnotationsContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#annotation}.
	 * @param ctx the parse tree
	 */
	void enterAnnotation(SneakParser.AnnotationContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#annotation}.
	 * @param ctx the parse tree
	 */
	void exitAnnotation(SneakParser.AnnotationContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#params}.
	 * @param ctx the parse tree
	 */
	void enterParams(SneakParser.ParamsContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#params}.
	 * @param ctx the parse tree
	 */
	void exitParams(SneakParser.ParamsContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#paramlist}.
	 * @param ctx the parse tree
	 */
	void enterParamlist(SneakParser.ParamlistContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#paramlist}.
	 * @param ctx the parse tree
	 */
	void exitParamlist(SneakParser.ParamlistContext ctx);
	/**
	 * Enter a parse tree produced by {@link SneakParser#param}.
	 * @param ctx the parse tree
	 */
	void enterParam(SneakParser.ParamContext ctx);
	/**
	 * Exit a parse tree produced by {@link SneakParser#param}.
	 * @param ctx the parse tree
	 */
	void exitParam(SneakParser.ParamContext ctx);
}